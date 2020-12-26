using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Api.Domain.Dtos;
using src.Api.Domain.Interfaces.Services.User;

namespace src.Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Login (
            [FromBody] LoginDTO loginDTO,
            [FromServices] ILoginService service)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            if(loginDTO == null) return BadRequest();

            try
            {
               var result = await service.FindByLogin(loginDTO);
               if(result != null) return Ok(result);
               return NotFound();
            }
            catch (ArgumentException e)
            {                
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            };
        }       
    }
}