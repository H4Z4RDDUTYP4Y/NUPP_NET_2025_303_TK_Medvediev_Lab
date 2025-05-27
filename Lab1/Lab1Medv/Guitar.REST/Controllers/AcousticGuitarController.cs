using AutoMapper;
using Guitar.Common.Crud;
using Guitar.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Guitar.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcousticGuitarController : ControllerBase
    {
        private readonly ICrudServiceAsync<AcousticModel> _service;
        private readonly IMapper _mapper;

        public AcousticGuitarController(ICrudServiceAsync<AcousticModel> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [AllowAnonymous] // Перегляд доступний всім
        [HttpGet("{id}")]
        public async Task<ActionResult<AcousticGuitarDto>> Get(Guid id)
        {
            var model = await _service.ReadAsync(id);
            if (model == null) return NotFound();
            return Ok(_mapper.Map<AcousticGuitarDto>(model));
        }

        [Authorize(Roles = "Admin,Archiver")] // Створювати можуть Admin і Archiver
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AcousticGuitarDto dto)
        {
            var model = _mapper.Map<AcousticModel>(dto);
            var result = await _service.CreateAsync(model);
            if (!result) return BadRequest();
            await _service.SaveAsync();
            return CreatedAtAction(nameof(Get), new { id = model.Id }, dto);
        }

        [Authorize(Roles = "Admin,Archiver")] // Редагувати можуть Admin і Archiver
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AcousticGuitarDto dto)
        {
            var model = await _service.ReadAsync(id);
            if (model == null) return NotFound();

            _mapper.Map(dto, model);
            await _service.UpdateAsync(model);
            await _service.SaveAsync();
            return NoContent();
        }

        [Authorize(Roles = "Admin")] // Видаляти може лише Admin
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = await _service.ReadAsync(id);
            if (model == null) return NotFound();

            await _service.DeleteAsync(id);
            await _service.SaveAsync();
            return NoContent();
        }
    }
}