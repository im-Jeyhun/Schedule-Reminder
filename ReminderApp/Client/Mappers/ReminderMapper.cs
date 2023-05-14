using AutoMapper;
using ReminderApp.Client.DTOs.Reminder;
using ReminderApp.DataBase.Models;

namespace ReminderApp.Client.Mappers
{
    public class ReminderMapper : Profile
    {
        public ReminderMapper()
        {
            //mapper for get list
            CreateMap<Reminder, ListItemDto>();

            //mapper for add
            CreateMap<AddDto, Reminder>();

            //mapper for get invidual
            CreateMap<ListItemDto, Reminder>();

            //mapper for update
            CreateMap<UpdateDto, Reminder>()
                .ForMember(r => r.Id, opt => opt.Ignore());
        }
    }
}
