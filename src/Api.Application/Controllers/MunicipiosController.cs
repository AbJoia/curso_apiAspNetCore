using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Api.Domain.Dtos.Municipio;
using src.Api.Domain.Interfaces.Services.Municipio;

namespace src.Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipiosController : ControllerBase
    {
        private IMunicipioService _service;

        public MunicipiosController(IMunicipioService service)
        {
            _service = service;            
        }

        [Authorize("Bearer")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await _service.Delete(id);
                if(!result) return NotFound();
                return Ok(result);
            }
            catch (ArgumentException e)
            {                
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("{id}", Name = "GetMunicipioWithId")]
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

        [Authorize("Bearer")]
        [HttpGet]        
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                return Ok(await _service.GetAll());
            }
            catch (ArgumentException e)
            {                
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("byIBGE/{codIBGE}")]
        public async Task<IActionResult> GetCompletoByIBGE(int codIBGE)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await _service.GetCompletoByIBGE(codIBGE);
                if(result == null) return NotFound();
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);                
            }
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("Complete/{id}")]
        public async Task<IActionResult> GetCompletoById(Guid id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await _service.GetCompletoById(id);
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
        public async Task<IActionResult> Post([FromBody] MunicipioDtoCreate dtoCreate)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await _service.Post(dtoCreate);
                if(result == null) return BadRequest();
                return Created(new Uri(Url.Link("GetMunicipioWithId", new {id = result.Id})), result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);                
            }
        }

        [Authorize("Bearer")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MunicipioDtoUpdate dtoUpdate)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await _service.Put(dtoUpdate);
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