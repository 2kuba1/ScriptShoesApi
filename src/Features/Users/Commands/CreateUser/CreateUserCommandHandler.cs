using AutoMapper;
using MediatR;
using ScriptShoesAPI.Database;
using ScriptShoesAPI.Database.Entities;

namespace ScriptShoesAPI.Features.Users.Commands.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Unit>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateUserHandler(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        var passwordHash =  HashPassword(request.Password);
        
        user.HashedPassword = passwordHash;
        user.RoleId = 2;
        user.ProfilePictureUrl = "https://cdn.discordapp.com/attachments/1023999045536059404/1039613165207568404/defaultAvatar.png";

        await _dbContext.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}