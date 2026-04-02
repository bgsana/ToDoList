using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models.DTOs;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController(UsuarioService usuarioService) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<UsuarioResponseDto>>> Get() =>
            Ok(await usuarioService.GetAllAsync());
    }
}