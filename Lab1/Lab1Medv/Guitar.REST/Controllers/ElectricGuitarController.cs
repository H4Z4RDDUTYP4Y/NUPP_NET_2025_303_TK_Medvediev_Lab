using AutoMapper;
using Guitar.Common.Crud;
using Guitar.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Guitar.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElectricGuitarController : ControllerBase
    {
        private readonly ICrudServiceAsync<ElectricModel> _service;
        private readonly IMapper _mapper;

        public ElectricGuitarController(ICrudServiceAsync<ElectricModel> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ElectricGuitarDto>> Get(Guid id)
        {
            var model = await _service.ReadAsync(id);
            if (model == null) return NotFound();
            return Ok(_mapper.Map<ElectricGuitarDto>(model));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ElectricGuitarDto dto)
        {
            var model = _mapper.Map<ElectricModel>(dto);
            var result = await _service.CreateAsync(model);
            if (!result) return BadRequest();
            await _service.SaveAsync();
            return CreatedAtAction(nameof(Get), new { id = model.Id }, dto);
        }

    }
}
