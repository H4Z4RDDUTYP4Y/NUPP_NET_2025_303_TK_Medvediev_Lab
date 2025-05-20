using AutoMapper;
using Guitar.Infrastructure.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ElectricGuitarDto, ElectricModel>().ReverseMap();
        CreateMap<AcousticGuitarDto, AcousticModel>().ReverseMap();
    }
}