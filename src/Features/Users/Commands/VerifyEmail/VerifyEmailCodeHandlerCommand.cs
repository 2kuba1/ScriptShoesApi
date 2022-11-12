using MediatR;
using Microsoft.EntityFrameworkCore;
using ScriptShoesApi.Exceptions;
using ScriptShoesCQRS.Database;
using ScriptShoesCQRS.Services.UserContext;

namespace ScriptShoesCQRS.Features.Users.Commands.VerifyEmail;

public class VerifyEmailCodeHandlerCommand : IRequestHandler<VerifyEmailCode, Unit>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _contextService;

    public VerifyEmailCodeHandlerCommand(AppDbContext dbContext, IUserContextService contextService)
    {
        _dbContext = dbContext;
        _contextService = contextService;
    }
    
    public async Task<Unit> Handle(VerifyEmailCode request, CancellationToken cancellationToken)
    {
        var getUserAndCode = await _dbContext.EmailCodes.FirstOrDefaultAsync(c => c.UserId == _contextService.GetUserId.Value && c.GeneratedCode == request.Code, cancellationToken: cancellationToken);
        if (getUserAndCode is null)
        {
            throw new NotFoundException("Code not found");
        }

        if (getUserAndCode.CodeExpires < DateTime.Now)
        {
            throw new NotFoundException("Code has expired");
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == _contextService.GetUserId.Value, cancellationToken: cancellationToken);
        user.IsActivated = true;
        _dbContext.EmailCodes.Remove(getUserAndCode);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}