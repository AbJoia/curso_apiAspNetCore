using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Api.Domain.Interfaces.Services.Uf;

namespace src.Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UfsController : ControllerBase
    {
        private IUfService _service;

        public UfsController(IUfService service)
        {
            _service = service;
        }

        [Authorize("Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(! ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                return Ok(await _service.GetAllAsync());
            }
            catch (ArgumentException e)
            {                
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try
            {                
                return Ok(await _service.GetAsync(id));
            }
            catch (ArgumentException e)
            {                
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        } 
    }
}