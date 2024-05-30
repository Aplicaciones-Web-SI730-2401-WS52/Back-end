using _1_API.Request;
using _3_Data;
using AutoMapper;

namespace _1_API.Mapper;

public class ModelsToRequest : Profile
{
    public ModelsToRequest()
    {
        CreateMap<Tutorial, TutorialRequest>();
    }
}