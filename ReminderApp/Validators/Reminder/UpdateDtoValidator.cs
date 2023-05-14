using FluentValidation;
using ReminderApp.Client.DTOs.Reminder;

namespace ReminderApp.Validators.Reminder
{
    public class UpdateDtoValidator : AbstractValidator<UpdateDto>
    {
        public UpdateDtoValidator()
        {
            RuleFor(ud => ud.To)
                .NotNull()
                .WithMessage("To can't be empty")
                .NotEmpty()
                .WithMessage("To can't be empty")
                .MinimumLength(5)
                .WithMessage("Minimum length should be 5")
                .MaximumLength(45)
                .WithMessage("Maximum length should be 30");

            RuleFor(ud => ud.Content)
              .NotNull()
              .WithMessage("Content can't be empty")
              .NotEmpty()
              .WithMessage("Content can't be empty")
              .MinimumLength(5)
              .WithMessage("Minimum length should be 5")
              .MaximumLength(45)
              .WithMessage("Maximum length should be 30");

            RuleFor(ud => ud.Method)
              .NotNull()
              .WithMessage("To can't be empty")
              .NotEmpty()
              .WithMessage("To can't be empty")
              .MaximumLength(45)
              .WithMessage("Maximum length should be 30");


            RuleFor(ud => ud.SendAt)
                .Must(date => date != default(DateTime))
                .WithMessage("Send date cant set for past time");
              
        }

    }
}
