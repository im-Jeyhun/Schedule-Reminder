﻿namespace ReminderApp.Exceptions
{
    public class ForbiddenException : ApplicationException
    {
        public ForbiddenException(string message)
            : base(message)
        {

        }
    }
}
