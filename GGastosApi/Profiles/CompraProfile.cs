using AutoMapper;
using GGastosApi.Data;
using GGastosApi.Models;

namespace GGastosApi.Profiles;

public class CompraProfile : Profile
{
    public CompraProfile()
    {
        CreateMap<CreateCompraDto, Compra>();
        CreateMap<UpdateCompraDto, Compra>();
        CreateMap<Compra, UpdateCompraDto>();
        CreateMap<Compra, ReadCompraDto>();
    }
}
