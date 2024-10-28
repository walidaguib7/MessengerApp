using Messenger.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Database;

public class DBContext : IdentityDbContext<User>
{
    public DBContext(DbContextOptions options) : base(options)
    {
        
    }
}