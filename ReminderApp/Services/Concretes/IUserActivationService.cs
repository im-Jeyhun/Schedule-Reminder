using ReminderApp.DataBase.Models;

namespace ReminderApp.Services.Concretes;

public interface IUserActivationService
{
    Task SendActivationUrlAsync(User user);
}