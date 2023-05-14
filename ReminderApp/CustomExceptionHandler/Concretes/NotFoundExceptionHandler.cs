using ReminderApp.CustomExceptionHandler.Abstracts;
using ReminderApp.DTOs;
using ReminderApp.Exceptions;
using System.Net;
using System.Net.Mime;

namespace ReminderApp.CustomExceptionHandler.Concretes;


public class NotFoundExceptionHandler : IExceptionHandler
{
    public ExceptionResultDto Handle(Exception exception)
    {
        var notFoundException = (NotFoundException)exception;

        return new ExceptionResultDto(MediaTypeNames.Text.Plain, (int)HttpStatusCode.NotFound, notFoundException.Message);
    }
}