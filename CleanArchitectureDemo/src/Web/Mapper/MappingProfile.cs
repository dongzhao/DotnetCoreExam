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
                .ForMember(t => t.Status, s => s.MapFrom(d => d.Status))
                .ForMember(t => t.ShippingAddress, s => s.MapFrom(d => d.ShippingAddress))
                .ForMember(t => t.ResidentAddress, s => s.MapFrom(d => d.ResidentAddress))
                .ForMember(t => t.Recipient, s => s.MapFrom(d => d.Recipient))
                .ForMember(t => t.CreditCard, s => s.MapFrom(d => d.CreditCard))
                .ForMember(t => t.CardType, s => s.MapFrom(d => d.CardType))
                .ForMember(t => t.OrderItems, s => s.MapFrom(d => d.OrderItems))
                .AfterMap<DataTransformAction<Order, OrderViewModel>>()
                .ReverseMap();
        }
    }
}
