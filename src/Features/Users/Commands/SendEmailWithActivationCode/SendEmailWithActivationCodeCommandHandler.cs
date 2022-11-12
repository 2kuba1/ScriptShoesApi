using System.Security.Cryptography;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesApi.Entities;
using ScriptShoesApi.Exceptions;
using ScriptShoesCQRS.Database;
using ScriptShoesCQRS.Services.EmailSender;
using ScriptShoesCQRS.Services.UserContext;

namespace ScriptShoesCQRS.Features.Users.Commands.SendEmailWithActivationCode;

public class SendEmailWithActivationCodeCommandHandler : IRequestHandler<SendEmailWithActivationCodeCommand, Unit>
{
    private readonly AppDbContext _dbContext;
    private readonly IEmailSenderService _emailSenderService;
    private readonly IUserContextService _userContextService;

    public SendEmailWithActivationCodeCommandHandler(AppDbContext dbContext, IEmailSenderService emailSenderService,
        IUserContextService userContextService)
    {
        _dbContext = dbContext;
        _emailSenderService = emailSenderService;
        _userContextService = userContextService;
    }

    public async Task<Unit> Handle(SendEmailWithActivationCodeCommand request, CancellationToken cancellationToken)
    {
        var code = Convert.ToBase64String(RandomNumberGenerator.GetBytes(6));

        await _emailSenderService.SendEmail(_userContextService.User.FindFirst(c => c.Type == "Email").Value, code,
            request.Subject);

        var newEmail = new EmailCodes()
        {
            GeneratedCode = code,
            CodeCreated = DateTime.Now,
            CodeExpires = DateTime.Now.AddMinutes(30),
            UserId = _userContextService.GetUserId.Value
        };
        
        await _dbContext.EmailCodes.AddAsync(newEmail, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}