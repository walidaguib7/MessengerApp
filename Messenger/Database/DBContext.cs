using Messenger.Config;
using Messenger.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Database;

public class DBContext : IdentityDbContext<User>
{
    public DBContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Blocking> blockings { get; set; }
    public DbSet<Contacts> contacts { get; set; }
    public DbSet<Conversation> conversations { get; set; }
    public DbSet<Files> files { get; set; }
    public DbSet<FriendRequest> FriendRequests { get; set; }
    public DbSet<Members> members { get; set; }
    public DbSet<Messages> messages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ConfigureRelationships();
    }
}