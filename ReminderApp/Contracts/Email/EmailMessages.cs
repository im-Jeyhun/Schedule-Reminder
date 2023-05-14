namespace ReminderApp.Contracts.Email
{
    public class EmailMessages
    {
        public static class Subject
        {
            public const string ACTIVATION_MESSAGE = $"Hesabin aktivlesdirilmesi";
        }

        public static class Body
        {
            public const string ACTIVATION_MESSAGE = $"Sizin activation urliniz : {EmailMessageKeywords.ACTIVATION_URL}";
            public const string SCHEDULE_REMINDER = $"Schedule Reminder";

        }
    }
}
