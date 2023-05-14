using Microsoft.Extensions.Options;
using ReminderApp.Options;
using ReminderApp.Services.Concretes;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ReminderApp.Services.Services
{
    public class TelegramService : ITelegramService
    {
        private TelegramConfigOptions _telegramConfig;

        public TelegramService(IOptions<TelegramConfigOptions> telegramConfig)
        {
            _telegramConfig = telegramConfig.Value;
        }

        public async Task SendMessage(string chatId, string message)
        {
            TelegramBotClient TelegramBot = new TelegramBotClient(_telegramConfig.Key);
            await TelegramBot.SendTextMessageAsync(chatId, message);
        }
    }
}
