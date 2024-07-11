using AutoMapper;
using Order.API.Models;
using Order.DB.Entities;

namespace Order.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Create a map from the entity to the DTO
            CreateMap<DbOrder, DtoOrder>()
                .ForMember(dto => dto.Address, conf => conf.MapFrom(ol => ol.Address))
                .ForMember(dto => dto.UserId, conf => conf.MapFrom(ol => ol.UserId))
                .ForMember(dto => dto.OrderItems, conf => conf.MapFrom(ol => ol.OrderItems));

            // Create a map from the DTO to the entity
            CreateMap<DtoOrder, DbOrder>()
                .ForMember(db => db.Address, conf => conf.MapFrom(dto => dto.Address))
                .ForMember(db => db.UserId, conf => conf.MapFrom(dto => dto.UserId))
                .ForMember(db => db.OrderItems, conf => conf.MapFrom(dto => dto.OrderItems));

            // Assuming DtoOrderItem and DbOrderItems have the same property names
            CreateMap<DtoOrderItem, DbOrderItems>();
            CreateMap<DbOrderItems, DtoOrderItem>();
        }
    }
}