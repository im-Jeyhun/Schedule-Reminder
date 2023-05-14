using FluentValidation;
using ReminderApp.Client.DTOs.Reminder;

namespace ReminderApp.Validators.Reminder
{
    public class AddDtoValidator : AbstractValidator<AddDto>
    {
        public AddDtoValidator()
        {
            RuleFor(ad => ad.To)
                .NotNull()
                .WithMessage("To can't be empty")
                .NotEmpty()
                .WithMessage("To can't be empty")
                .MinimumLength(5)
                .WithMessage("Minimum length should be 5")
                .MaximumLength(45)
                .WithMessage("Maximum length should be 30");



            RuleFor(ad => ad.Content)
              .NotNull()
              .WithMessage("Content can't be empty")
              .NotEmpty()
              .WithMessage("Content can't be empty")
              .MinimumLength(5)
              .WithMessage("Minimum length should be 5")
              .MaximumLength(45)
              .WithMessage("Maximum length should be 30");

            RuleFor(ad => ad.Method)
              .NotNull()
              .WithMessage("To can't be empty")
              .NotEmpty()
              .WithMessage("To can't be empty")
              .MaximumLength(45)
              .WithMessage("Maximum length should be 30");


            RuleFor(ad => ad.SendAt)
                .Must(ad => BeAValidDate(ad.ToString()))
                .WithMessage("Send date cant set for past time")
                .NotEmpty()
                .WithMessage("To can't be empty");
              

        }

        private bool BeAValidDate(string value)
        {
            DateTime date;
            return DateTime.TryParse(value, out date);
        }
    }
}
