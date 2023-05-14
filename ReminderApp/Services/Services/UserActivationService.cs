using Microsoft.AspNetCore.Mvc;
using ReminderApp.Contracts.Email;
using ReminderApp.DataBase;
using ReminderApp.DataBase.Models;
using ReminderApp.Services.Concretes;

namespace ReminderApp.Services.Services;

public class UserActivationService : IUserActivationService
{
    private readonly DataContext _dataContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEmailService _emailService;
    private readonly IUrlHelper _urlHelper;
    private readonly DateTime _activationExpireDate;
    private const string EMAIL_CONFIRMATION_ROUTE_NAME = "auth-activate";


    public UserActivationService(
        DataContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IEmailService emailService,
        IUrlHelper urlHelper,
        IConfiguration configuration)
    {
        _dataContext = dataContext;
        _httpContextAccessor = httpContextAccessor;
        _emailService = emailService;
        _urlHelper = urlHelper;

        double activationValidityMinute =
            Convert.ToDouble(configuration.GetRequiredSection("ActivationValidityMinute").Value);

        _activationExpireDate = DateTime.Now.AddMinutes(activationValidityMinute);
    }



    public async Task SendActivationUrlAsync(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        var token = GenerateActivationToken();
        var activationUrl = GenerateUrl(token, EMAIL_CONFIRMATION_ROUTE_NAME);
        await CreateUserActivationAsync(user, token, activationUrl, _activationExpireDate);
        var activationMessageDto = PrepareActivationMessage(user.Email!, activationUrl);

        _emailService.Send(activationMessageDto);
    }


    private string GenerateActivationToken()
    {
        return Guid.NewGuid().ToString();
    }

    private string GenerateUrl(string token, string routeName)
    {
        var request = _httpContextAccessor.HttpContext!.Request;
        return _urlHelper.RouteUrl(routeName, new { token }, request.Scheme, request.Host.Value)!;
    }

    private async Task<UserActivation> CreateUserActivationAsync(User user, string token, string activationUrL, DateTime expireDate)
    {
        var userActivation = new UserActivation
        {
            User = user,
            ActivationToken = token,
            ActivationUrl = activationUrL,
            ExpireDate = expireDate,
        };

        await _dataContext.UserActivations.AddAsync(userActivation);

        return userActivation;
    }

    private MessageDto PrepareActivationMessage(string email, string activationUrl)
    {
        string body = EmailMessages.Body.ACTIVATION_MESSAGE
            .Replace(EmailMessageKeywords.ACTIVATION_URL, activationUrl);

        string subject = EmailMessages.Subject.ACTIVATION_MESSAGE;

        return new MessageDto(email, subject, body);
    }
}