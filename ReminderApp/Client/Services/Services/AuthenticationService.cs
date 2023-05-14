using Microsoft.EntityFrameworkCore;
using ReminderApp.Client.DTOs.Authentication;
using ReminderApp.Client.Services.Concretes;
using ReminderApp.Contracts.ModelName;
using ReminderApp.DataBase;
using ReminderApp.Exceptions;
using ReminderApp.Services.Concretes;
using System.ComponentModel.DataAnnotations;
using ValidationException = ReminderApp.Exceptions.ValidationException;

namespace ReminderApp.Client.Services.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly DataContext _dataContext;
    public readonly IUserService _userService;

    public AuthenticationService(IUserService userService, DataContext dataContext)
    {
        _userService = userService;
        _dataContext = dataContext;
    }
    public async Task RegisterAsync(RegisterDto dto)
    {
        var user = await _dataContext.Users.SingleOrDefaultAsync(u => u.Email == dto.Email);

        if (user is not null) throw new ValidationException("Email already exists");

        var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        dto.Password = hash;
        await _userService.CreateAsync(dto);
    }

    public async Task ActivateAsync(string token)
    {
        var userActivation = await _dataContext.UserActivations
            .Include(ua => ua.User)
            .FirstOrDefaultAsync(ua => !ua!.User!.IsEmailConfirmed && ua.ActivationToken == token)
            ?? throw new NotFoundException(DomainModelNames.USER_ACTIVATION, token);

        if (DateTime.Now > userActivation!.ExpireDate) throw new ValidationException("Token expired olub teessufler");

        userActivation!.User!.IsEmailConfirmed = true;

        await _dataContext.SaveChangesAsync();
    }
}
