using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens; // ADICIONADO
using Scalar.AspNetCore;
using System.Text;
using ToDoList.Data;
using ToDoList.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"] ?? "RGeGugb+7aBcNCFtJ3z3PQHp6DpDUSjIdZHhjtdFWZueHTeMuvu0v6J4DRJjZYMSHgLzHueowPM7tbnmXPclmg==");

builder.Services.AddAuthentication(options =>
{
    // Define que o padrão de entrada e desafio é o JWT Bearer
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateLifetime = true
    };
});

builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro dos Services
builder.Services.AddScoped<TarefaService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ComentarioService>();
builder.Services.AddScoped<AuthService>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .ToDictionary(
                k => k.Key,
                v => v.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
            );

        return new BadRequestObjectResult(new
        {
            message = "Falha de validação.",
            errors
        });
    };
});

builder.Services.AddRateLimiter(options => {
    options.AddFixedWindowLimiter("fixed", opt => {
        opt.Window = TimeSpan.FromSeconds(10);
        opt.PermitLimit = 5;
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("API Aula 01 - .NET 10")
               .WithTheme(ScalarTheme.Moon)
               .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseCors("DevCors");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization(); 

app.UseRateLimiter();
app.MapControllers();

app.Run();