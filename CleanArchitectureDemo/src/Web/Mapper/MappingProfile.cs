using AutoMapper;
using Core.Entities;
using Web.Models;

namespace Web.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(t => t.OrderId, s => s.MapFrom(d => d.Id))
                .ForMember(t => t.OrderDate, s => s.MapFrom(d => d.CreatedDateTime))
                .ForMember(t => t.OrderBy, s => s.MapFrom(d => d.CreatedBy))
                .AfterMap<DataTransformAction<Order, OrderViewModel>>()
                .ReverseMap();
        }
    }
}
