using Messenger.Dtos.User;
using Messenger.Models;

namespace Messenger.Services;

public interface IAuth
{
    public Task<User?> Register(RegisterDto dto);
    public Task<UserDto?> Login(LoginDto dto);
}