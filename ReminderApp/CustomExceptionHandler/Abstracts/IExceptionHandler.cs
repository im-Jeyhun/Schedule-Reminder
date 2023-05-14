using ReminderApp.DTOs;

namespace ReminderApp.CustomExceptionHandler.Abstracts
{
    public interface IExceptionHandler
    {
        public ExceptionResultDto Handle(Exception exception);

    }
}
