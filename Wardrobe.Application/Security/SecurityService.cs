using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Wardrobe.CrossCutting;
using Wardrobe.CrossCutting.Exceptions;
using Wardrobe.Domain.Identity;
using Wardrobe.Infra.Database;

namespace Wardrobe.Application.Security;

public class SecurityService : ISecurityService
{
    private readonly IIdentityRepository _identityRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly ILogger<SecurityService> _logger;

    public SecurityService(
        IIdentityRepository identityRepository, 
        IUserRepository userRepository,
        ITokenService tokenService,
        ILogger<SecurityService> logger)
    {
        _identityRepository = identityRepository;
        _userRepository = userRepository;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task Register(string email, string password)
    {
        var existingUser = (await _userRepository
            .Get(Builders<Domain.Entities.User.User>.Filter.Eq(u => u.Email, email)))
            .FirstOrDefault();

        if (existingUser != null) throw new UserAlreadyExistsException(email);
        
        var user = await _userRepository.Save(new Domain.Entities.User.User
        {
            Email = email
        });

        await _identityRepository.Save(new Identity
        {
            User = user,
            Password = password.ConvertToMd5Hash()
        });
    }

    public async Task<(string Token, Domain.Entities.User.User user)> Login(string email, string password)
    {
        var user = await _userRepository.GetByEmail(email);
        if (user == null) throw new UnauthorizedUser(email);

        var userIdentity = await _identityRepository.GetUserIdentity(user.Id!);
        if(userIdentity == null) throw new UnauthorizedUser(email);

        if (password.ConvertToMd5Hash() != userIdentity.Password)
            throw new UnauthorizedUser(email);

        var token = _tokenService.GenerateToken(user.Id);

        return (token, user);
    }
}

public interface ISecurityService
{
    Task Register(string email, string password);
    Task<(string Token, Domain.Entities.User.User user)> Login(string email, string password);
}