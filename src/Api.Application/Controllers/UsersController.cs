using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Api.Domain.Dtos.User;
using src.Api.Domain.Interfaces.Services.User;

namespace src.Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]        
    public class UsersController : ControllerBase
    {
        private IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }

        [Authorize("Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetAll());
            }
            catch (ArgumentException e)
            {                
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route ("{id}", Name = "GetWithId")]
        public async Task<IActionResult> Get (Guid id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.Get(id);
                if(result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (ArgumentException e)
            {                
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] UserDTOCreate user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.Post(user);
                if(result != null)
                {
                   return Created (new Uri(Url.Link("GetWithId", new {id = result.Id})), result);     
                }else
                {
                   return BadRequest();     
                }
            }
            catch (ArgumentException e)
            {                
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserDTOUpdate user)
        {
            if(!ModelState.IsValid)
            {
               return BadRequest(ModelState); 
            }

            try
            {
               var result = await _service.Put(user);
                if(result != null)
                {
                    return Ok(result);                 
                }
                else
                {
                    return BadRequest();
                } 
            }
            catch (ArgumentException e)
            {
                
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
            
        }

        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (Guid id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.Delete(id));     
            }
            catch (ArgumentException e)
            {                
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message); 
            }
        }
    }
}