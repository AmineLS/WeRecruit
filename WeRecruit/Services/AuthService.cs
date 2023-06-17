using WeRecruit.Dto;
using WeRecruit.Entities;

namespace WeRecruit.Services;

public class AuthService : IAuthService
{
    private readonly HashSet<Admin> _admins;

    public AuthService(HashSet<Admin> admins)
    {
        _admins = admins;
    }

    public Task<Tuple<bool, Admin>> TryAuthenticateAdmin(LoginDto loginDto)
    {
        var adminExists =
            _admins.TryGetValue(new Admin { Identifier = loginDto.Identifier, Password = loginDto.Password },
                out var admin);

        return Task.FromResult(adminExists
            ? Tuple.Create(true, admin)!
            : Tuple.Create(false, new Admin()));
    }
}