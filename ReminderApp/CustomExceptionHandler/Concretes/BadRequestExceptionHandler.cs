using ReminderApp.CustomExceptionHandler.Abstracts;
using ReminderApp.DTOs;
using ReminderApp.Exceptions;
using System.Net;
using System.Net.Mime;

namespace ReminderApp.CustomExceptionHandler.Concretes
{
    public class BadRequestExceptionHandler : IExceptionHandler
    {
        public ExceptionResultDto Handle(Exception exception)
        {
            var badRequestException = (BadRequestException)exception;

            return new ExceptionResultDto(MediaTypeNames.Text.Plain, (int)HttpStatusCode.BadRequest, badRequestException.Message);
        }
    }
}
