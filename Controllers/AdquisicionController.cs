using Microsoft.AspNetCore.Mvc;
using Adres.Services.Interfaces;
using AutoMapper;
using Adres.DTOs;
using Adres.Models;

namespace Adres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdquisicionController : ControllerBase
    {
        private readonly IAdquisicionService _adquisicionService;
        private readonly IParametricaService _parametricaService;
        private readonly IMapper _mapper;

        public AdquisicionController(IAdquisicionService adquisicionService, IMapper mapper, IParametricaService parametricaService)
        {
            _adquisicionService = adquisicionService;
            _mapper = mapper;
            _parametricaService = parametricaService;
        }

        // GET: api/Adquisicion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdquisicionDTO>>> GetAdquisiciones()
        {
            var listaAdquisiciones = await _adquisicionService.GetList();
            var listaAdquisicionesDTO = _mapper.Map<List<AdquisicionDTO>>(listaAdquisiciones);
            if(listaAdquisicionesDTO.Count>0)
                return Ok(listaAdquisicionesDTO);
            else
                return NoContent();
        }

        // GET: api/Adquisicion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdquisicionDTO>> GetAdquisicion(int id)
        {
            var resultadoAdquisicion = await _adquisicionService.Get(id);
            if(resultadoAdquisicion is null)
                return NoContent();
            else
                return Ok(_mapper.Map<AdquisicionDTO>(resultadoAdquisicion));
        }

        // POST: api/Adquisicion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdquisicionDTO>> PostAdquisicion(AdquisicionDTO model)
        {
            var adquisicion = _mapper.Map<Adquisicion>(model);
            var adquisicionAdd = await _adquisicionService.Add(adquisicion);
            if(adquisicionAdd.Id != 0)
            {
                return Ok(_mapper.Map<AdquisicionDTO>(adquisicionAdd));
            }
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // PUT: api/Adquisicion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<AdquisicionDTO>> PutAdquisicion(int id, AdquisicionDTO model)
        {
            var resultadoAdquisicion = await _adquisicionService.Get(id);
            if(resultadoAdquisicion is null)
                return NoContent();

            var adquisicion = _mapper.Map<Adquisicion>(model);
            resultadoAdquisicion.Presupuesto = adquisicion.Presupuesto;
            resultadoAdquisicion.UnidadId = adquisicion.UnidadId;
            resultadoAdquisicion.BienId = adquisicion.BienId;
            resultadoAdquisicion.Cantidad = adquisicion.Cantidad;
            resultadoAdquisicion.ValorUnitario = adquisicion.ValorUnitario;
            resultadoAdquisicion.ValorTotal = adquisicion.ValorTotal;
            resultadoAdquisicion.Fecha = adquisicion.Fecha;
            resultadoAdquisicion.ProveedorId = adquisicion.ProveedorId;
            resultadoAdquisicion.Documentacion = adquisicion.Documentacion;

            var resultadoUpdate = await _adquisicionService.Update(resultadoAdquisicion);

            if(resultadoUpdate)
                return Ok(_mapper.Map<AdquisicionDTO>(resultadoAdquisicion));
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
            
        }

        // DELETE: api/Adquisicion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdquisicion(int id)
        {
            var resultadoAdquisicion = await _adquisicionService.Get(id);
            if(resultadoAdquisicion is null)
                return NoContent();

            var resultadoDelete = await _adquisicionService.Delete(resultadoAdquisicion);

            if(resultadoDelete)
                return Ok();
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("/parametrica")]
        public async Task<ActionResult<IEnumerable<Parametrica>>> GetParametricas()
        {
            return await _parametricaService.GetList();
        }

        [HttpGet("/adquisiciones")]
        public async Task<ActionResult<IEnumerable<Adquisicion>>> Get()
        {
            return await _adquisicionService.GetList();
        }
    }
}
