using AutoMapper;
using Guitar.Common.Crud;
using Guitar.Infrastructure.Models;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<AcousticGuitarDto>> Get(Guid id)
        {
            var model = await _service.ReadAsync(id);
            if (model == null) return NotFound();
            return Ok(_mapper.Map<ElectricGuitarDto>(model));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AcousticGuitarDto dto)
        {
            var model = _mapper.Map<AcousticModel>(dto);
            var result = await _service.CreateAsync(model);
            if (!result) return BadRequest();
            await _service.SaveAsync();
            return CreatedAtAction(nameof(Get), new { id = model.Id }, dto);
        }

    }
}
