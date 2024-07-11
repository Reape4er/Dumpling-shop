using AutoMapper;
using Users.API.Models;
using Users.db.Entities;

namespace Users.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<DtoUser, DbUser>().ReverseMap();
            CreateMap<DtoUserRegistration, DbUser>();
            CreateMap<DbUser, DtoAuthenticationResponse>();
        }
    }
}
