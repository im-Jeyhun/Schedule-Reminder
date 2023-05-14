using ReminderApp.Contracts.Email;
using ReminderApp.DataBase;
using ReminderApp.DataBase.Models.Enums;
using ReminderApp.Services.Concretes;
using ReminderApp.Services.Services;

namespace ReminderApp.BackgroundService
{
    public class ReminderService : IHostedService , IDisposable
    {
        private ILogger<ReminderService> _logger;

        public IServiceProvider Services { get; }

        private Timer _timer;

        public ReminderService(ILogger<ReminderService> logger, IServiceProvider services)
        {
            _logger = logger;
            Services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(ExecuteCurrentRemind, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
            return Task.CompletedTask;


        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            _timer.Dispose();

        }

        private void ExecuteCurrentRemind(object state)
        {
            using IServiceScope scope = Services.CreateScope();

            var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
            var telegramService = scope.ServiceProvider.GetRequiredService<ITelegramService>();

            try
            {
                var reminders = dataContext.Reminders.Where(r => r.SendAt == DateTime.Now).ToList();

                foreach (var reminder in reminders)
                {
                    if(reminder.Method == ReminderMethod.Email)
                    {
                        emailService.Send(new Contracts.Email.MessageDto(reminder.To, EmailMessages.Body.SCHEDULE_REMINDER, reminder.Content));
                    }
                    else if(reminder.Method == ReminderMethod.Telegram)
                    {
                        telegramService.SendMessage(reminder.To , reminder.Content);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Something went wrong while {nameof(ReminderService)} working");
                throw new Exception(e.Message);
            }

        }
    }
}
