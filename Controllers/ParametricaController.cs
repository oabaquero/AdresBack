using Microsoft.AspNetCore.Mvc;
using Adres.Services.Interfaces;
using AutoMapper;
using Adres.DTOs;
using Adres.Models;

namespace Adres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametricaController : ControllerBase
    {
        private readonly IParametricaService _parametricaService;
        private readonly IMapper _mapper;

        public ParametricaController(IParametricaService parametricaService, IMapper mapper)
        {
            _mapper = mapper;
            _parametricaService = parametricaService;
        }

        // GET: api/Parametrica/Unidades
        [HttpGet("unidades")]
        public async Task<ActionResult<IEnumerable<ParametricaDTO>>> GetUnidades()
        {
            var listaUnidades = await _parametricaService.GetList("Unidad");
            var listaUnidadesDTO = _mapper.Map<List<ParametricaDTO>>(listaUnidades);
            if(listaUnidadesDTO.Count>0)
                return Ok(listaUnidadesDTO);
            else
                return NoContent();
        }

        // GET: api/Parametrica/Bienes
        [HttpGet("bienes")]
        public async Task<ActionResult<IEnumerable<ParametricaDTO>>> GetBienes()
        {
            var listaBienes = await _parametricaService.GetList("Bien/Servicio");
            var listaBienesDTO = _mapper.Map<List<ParametricaDTO>>(listaBienes);
            if(listaBienesDTO.Count>0)
                return Ok(listaBienesDTO);
            else
                return NoContent();
        }

        // GET: api/Parametrica/Bienes
        [HttpGet("proveedores")]
        public async Task<ActionResult<IEnumerable<ParametricaDTO>>> GetProveedores()
        {
            var listaProveedores = await _parametricaService.GetList("Laboratorio");
            var listaProveedoresDTO = _mapper.Map<List<ParametricaDTO>>(listaProveedores);
            if(listaProveedoresDTO.Count>0)
                return Ok(listaProveedoresDTO);
            else
                return NoContent();
        }
    }
}
