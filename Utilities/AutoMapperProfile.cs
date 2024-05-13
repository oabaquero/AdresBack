using System.Globalization;
using Adres.DTOs;
using Adres.Models;
using AutoMapper;

namespace Adres.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Adquisicion, AdquisicionDTO>()
            .ForMember(to => to.NombreUnidad,
            from => from.MapFrom(origin => origin.Unidades.Nombre))
            .ForMember(to => to.NombreBien,
            from => from.MapFrom(origin => origin.Bienes.Nombre))
            .ForMember(to => to.NombreProveedor,
            from => from.MapFrom(origin => origin.Proveedores.Nombre))
            .ForMember(to => to.Fecha,
            from => from.MapFrom(origin => origin.Fecha.ToString("dd/MM/yyyy")));

            CreateMap<AdquisicionDTO, Adquisicion>()
            .ForMember(to => to.Bienes,
            from => from.Ignore())
            .ForMember(to => to.Unidades,
            from => from.Ignore())
            .ForMember(to => to.Proveedores,
            from => from.Ignore())
            .ForMember(to => to.Fecha,
            from => from.MapFrom(origin => DateTime.ParseExact(origin.Fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture)));

            CreateMap<Parametrica, ParametricaDTO>().ReverseMap();

             CreateMap<Historico, HistoricoDTO>()
            .ForMember(to => to.FechaModificacion,
            from => from.MapFrom(origin => origin.FechaModificacion.ToString("dd/MM/yyyy")));

            CreateMap<HistoricoDTO, Historico>()
            .ForMember(to => to.FechaModificacion,
            from => from.MapFrom(origin => DateTime.ParseExact(origin.FechaModificacion, "dd/MM/yyyy", CultureInfo.InvariantCulture)));
        }

    }
}