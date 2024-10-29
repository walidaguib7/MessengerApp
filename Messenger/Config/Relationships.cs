using Messenger.Models;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Config;

public static class Relationships
{
    public static void ConfigureRelationships(this ModelBuilder builder)
    {
        //Blocking  & User Models
        builder.Entity<Blocking>(bu => bu.HasKey(bu => new { bu.userId, bu.blockedUserId }));
        builder.Entity<User>()
            .HasMany(f => f.blockedusers)
            .WithOne(f => f.user)
            .HasForeignKey(f => f.userId);
        //Contacts & User Models
        builder.Entity<Contacts>(c => c.HasKey(c => new { c.userId, c.friendId }));
        builder.Entity<User>()
            .HasMany(c => c.Friends)
            .WithOne(c => c.user)
            .HasForeignKey(c => c.userId);
        //Conversation & users (Members relationship)
        builder.Entity<Members>(m => m.HasKey(m => new { m.userId, m.conversationId }));
        builder.Entity<Members>()
            .HasOne(m => m.user)
            .WithMany(m => m.MembersList)
            .HasForeignKey(m => m.userId);
        builder.Entity<Members>()
            .HasOne(m => m.conversation)
            .WithMany(m => m.members)
            .HasForeignKey(m => m.conversationId);
    }
}