
using ReminderApp.Client.DTOs.Authentication;

namespace ReminderApp.Client.Services.Concretes;

public interface IAuthenticationService
{
    Task RegisterAsync(RegisterDto dto);
    Task ActivateAsync(string token);
}
