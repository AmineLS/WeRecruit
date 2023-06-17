using WeRecruit.Dto;
using WeRecruit.Entities;

namespace WeRecruit.Services;

public interface IAuthService
{
    Task<Tuple<bool, Admin>> TryAuthenticateAdmin(LoginDto loginDto);
}