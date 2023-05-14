using ReminderApp.Contracts.Email;

namespace ReminderApp.Services.Concretes;

public interface IEmailService
{
    public void Send(MessageDto messageDto);
}