namespace ReminderApp.DataBase.Models.Enums
{
    public enum ReminderMethod
    {
        Telegram = 0,
        Email = 1,
    }

    public static class ReminderMethodName
    {
        public static string GetStatusCode(this ReminderMethod status)
        {
            switch (status)
            {
                case ReminderMethod.Telegram:
                    return "Telegram";
                case ReminderMethod.Email:
                    return "Email";
                default:
                    throw new Exception("This method name is not found");
            }
        }
    }
}
