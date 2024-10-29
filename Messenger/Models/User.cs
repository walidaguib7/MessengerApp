using Microsoft.AspNetCore.Identity;

namespace Messenger.Models;

public class User : IdentityUser
{
    public int fileId { get; set; }
    public Files? file { get; set; }

    public List<Blocking> blockedusers { get; set; } = [];
    public List<Contacts> Friends { get; set; } = [];
    public List<Members> MembersList { get; set; } = [];
}