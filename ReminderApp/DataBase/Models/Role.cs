using ReminderApp.DataBase.Models.Common;

namespace ReminderApp.DataBase.Models;

public class Role: BaseEntity<int>, IAuditable
{
    public string Name { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<User>? Users { get; set; }
}
