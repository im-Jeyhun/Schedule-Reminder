using AutoMapper;
using ReminderApp.Client.DTOs.Authentication;
using ReminderApp.DataBase.Models;

namespace ReminderApp.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<RegisterDto, User>();

            CreateMap<LoginDto, User>();

        }

    }
}
