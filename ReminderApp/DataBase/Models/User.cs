using ReminderApp.DataBase.Models.Common;

namespace ReminderApp.DataBase.Models;

public class User: BaseEntity<int>, IAuditable
{
    public string Email { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Password { get; set; } = default!;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


    public int? RoleId { get; set; }
    public Role? Role { get; set; }
    public int? UserActivationId { get; set; }

    public UserActivation? UserActivation { get; set; }
    public bool IsEmailConfirmed { get; set; }
}
