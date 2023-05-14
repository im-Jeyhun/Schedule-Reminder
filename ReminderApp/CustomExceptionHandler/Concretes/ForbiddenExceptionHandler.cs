using ReminderApp.CustomExceptionHandler.Abstracts;
using ReminderApp.DTOs;
using ReminderApp.Exceptions;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace ReminderApp.CustomExceptionHandler.Concretes;

public class ForbiddenExceptionHandler : IExceptionHandler
{
    public ExceptionResultDto Handle(Exception exception)
    {
        var forbiddenException = (ForbiddenException)exception;

        return new ExceptionResultDto(MediaTypeNames.Application.Json, (int)HttpStatusCode.Forbidden, JsonSerializer.Serialize(forbiddenException.Message));
    }
}
