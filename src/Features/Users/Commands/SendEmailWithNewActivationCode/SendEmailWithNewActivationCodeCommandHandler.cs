using System.Security.Cryptography;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesAPI.Database;
using ScriptShoesAPI.Database.Entities;
using ScriptShoesApi.Exceptions;
using ScriptShoesAPI.Services.EmailSender;
using ScriptShoesAPI.Services.UserContext;

namespace ScriptShoesAPI.Features.Users.Commands.SendEmailWithNewActivationCode;

public class SendEmailWithNewActivationCodeCommandHandler : IRequestHandler<SendEmailWithNewActivationCodeCommand, Unit>
{
    private readonly IEmailSenderService _emailSenderService;
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _contextService;

    public SendEmailWithNewActivationCodeCommandHandler(IEmailSenderService emailSenderService, AppDbContext dbContext, IUserContextService contextService)
    {
        _emailSenderService = emailSenderService;
        _dbContext = dbContext;
        _contextService = contextService;
    }
    
    public async Task<Unit> Handle(SendEmailWithNewActivationCodeCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == _contextService.GetUserId.Value, cancellationToken: cancellationToken);
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }
        
        if (user.IsActivated)
        {
            throw new ConflictException("You are already verified");
        }
        
        var getOlderCode = await _dbContext.EmailCodes.FirstOrDefaultAsync(e => e.UserId == user.Id, cancellationToken: cancellationToken);
        if (getOlderCode is null)
        {
            throw new ConflictException("You didn't send any verify requests before");
        }
        _dbContext.EmailCodes.Remove(getOlderCode);
        await _dbContext.SaveChangesAsync(cancellationToken);
        await SendEmailWithCode(user.Email, "Script shoes verification email", user.Id);
        
        return Unit.Value;
    }
    
    private async Task SendEmailWithCode(string userEmail, string subject, int userId)
    {
        var code = Convert.ToBase64String(RandomNumberGenerator.GetBytes(6));

        await _emailSenderService.SendEmail(userEmail, code, subject);

        var newEmail = new EmailCodes()
        {
            GeneratedCode = code,
            CodeCreated = DateTime.Now,
            CodeExpires = DateTime.Now.AddMinutes(30),
            UserId = userId
        };

        await _dbContext.EmailCodes.AddAsync(newEmail);
        await _dbContext.SaveChangesAsync();
    }
}