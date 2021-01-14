using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Api.Domain.Dtos.Cep;
using src.Api.Domain.Interfaces.Services.Cep;

namespace src.Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CepsController : ControllerBase
    {
        private ICepService _service;

        public CepsController(ICepService service)
        {
            _service = service;
        }

        [Authorize("Bearer")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if(!ModelState.IsValid) return BadRequest();
            try
            {
                return Ok(await _service.Delete(id));
            }
            catch (ArgumentException e)
            {                
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("{id}", Name = "GetCepWithId")]
        public async Task<IActionResult> Get(Guid id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await _service.Get(id);
                if(result == null) return NotFound();
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("byCep/{cep}")]
        public async Task<IActionResult> Get(string cep)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await _service.Get(cep);
                if(result == null) return NotFound();
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CepDtoCreate cepDtoCreate)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await _service.Post(cepDtoCreate);
                if(result == null) return BadRequest();
                return Created(new Uri(Url.Link("GetCepWithId", new {id = result.Id})),result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CepDtoUpdate cepUpdate)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await _service.Put(cepUpdate);
                if(result == null) return BadRequest();
                return Ok(result);
            }
             catch (ArgumentException e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }        
    }
}