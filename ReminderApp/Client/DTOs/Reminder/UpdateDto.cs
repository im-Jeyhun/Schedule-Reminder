using System.ComponentModel.DataAnnotations;

namespace ReminderApp.Client.DTOs.Reminder;

public class UpdateDto
{
    [Required]
    public string To { get; set; }

    [Required]

    public string Content { get; set; }

    [Required]
    public DateTime SendAt { get; set; }

    [Required]
    public string Method { get; set; }
}
