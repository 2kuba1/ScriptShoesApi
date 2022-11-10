using AutoMapper;
using MediatR;
using ScriptShoesApi.Entities;
using ScriptShoesCQRS.Database;

namespace ScriptShoesCQRS.Features.Users.Commands.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserQuery, Unit>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateUserHandler(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(CreateUserQuery request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request.dto);
        var passwordHash =  HashPassword(request.dto.Password);
        
        user.HashedPassword = passwordHash;
        user.RoleId = 1;

        await _dbContext.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}