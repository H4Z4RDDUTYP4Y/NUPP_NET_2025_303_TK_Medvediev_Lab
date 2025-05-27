using AutoMapper;
using Guitar.Infrastructure.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ElectricModel, ElectricGuitarDto>().ReverseMap();
        CreateMap<AcousticGuitarDto, AcousticModel>().ReverseMap();
    }
}