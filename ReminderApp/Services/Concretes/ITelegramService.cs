namespace ReminderApp.Services.Concretes
{
    public interface ITelegramService
    {
        public Task SendMessage(string chatId, string message);
    }
}
