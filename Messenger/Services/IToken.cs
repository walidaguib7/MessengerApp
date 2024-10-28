using Messenger.Models;

namespace Messenger.Services;

public interface IToken
{
    public string CreateToken(User user);
}