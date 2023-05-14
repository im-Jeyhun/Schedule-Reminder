using ReminderApp.DataBase.Models.Common;
using ReminderApp.DataBase.Models.Enums;

namespace ReminderApp.DataBase.Models
{
    public class Reminder : BaseEntity<int> , IAuditable
    {
        public string To { get; set; }
        public string Content { get; set; }
        public DateTime SendAt { get; set; }
        public ReminderMethod Method { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
