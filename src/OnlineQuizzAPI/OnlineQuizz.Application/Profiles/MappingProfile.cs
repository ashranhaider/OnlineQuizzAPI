using AutoMapper;
using OnlineQuizz.Application.Features.Categories.Commands.CreateCateogry;
using OnlineQuizz.Application.Features.Categories.Queries.GetCategoriesList;
using OnlineQuizz.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using OnlineQuizz.Application.Features.Events.Commands.CreateEvent;
using OnlineQuizz.Application.Features.Events.Commands.UpdateEvent;
using OnlineQuizz.Application.Features.Events.Queries.GetEventDetail;
using OnlineQuizz.Application.Features.Events.Queries.GetEventsExport;
using OnlineQuizz.Application.Features.Events.Queries.GetEventsList;
using OnlineQuizz.Application.Features.Orders.GetOrdersForMonth;
using OnlineQuizz.Domain.Entities;

namespace OnlineQuizz.Application.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventListVm>().ReverseMap();
            CreateMap<Event, CreateEventCommand>().ReverseMap();
            CreateMap<Event, UpdateEventCommand>().ReverseMap();
            CreateMap<Event, EventDetailVm>().ReverseMap();
            CreateMap<Event, CategoryEventDto>().ReverseMap();
            CreateMap<Event, EventExportDto>().ReverseMap();

            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryListVm>();
            CreateMap<Category, CategoryEventListVm>();
            CreateMap<Category, CreateCategoryCommand>();
            CreateMap<Category, CreateCategoryDto>();

            CreateMap<Order, OrdersForMonthDto>();
        }
    }
}
