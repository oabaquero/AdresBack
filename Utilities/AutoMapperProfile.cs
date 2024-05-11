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
        }

    }
}