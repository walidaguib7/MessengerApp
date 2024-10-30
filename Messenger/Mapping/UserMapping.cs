using Messenger.Dtos.User;
using Messenger.Models;

namespace Messenger.Mapping;

public static class UserMapping
{
    public static User ToDto(this RegisterDto dto)
    {
        return new User
        {
          UserName = dto.username,
          Email = dto.email
        };
    }
}