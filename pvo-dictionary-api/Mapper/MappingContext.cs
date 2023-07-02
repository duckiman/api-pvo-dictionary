using AutoMapper;
using pvo_dictionary_api.Dto;
using pvo_dictionary_api.Models;
using pvo_dictionary_api.Request;

namespace pvo_dictionary_api.Mapper
{
    public class MappingContext : Profile
    {
        public MappingContext()
        {
            // user request
            CreateMap<UserRegisterRequest, User>();
            CreateMap<UserStoreRequest, User>();
        }
    }
}
