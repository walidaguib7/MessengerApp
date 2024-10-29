namespace Messenger.Models;

public class FriendRequest
{
    public int Id { get; set; }
    public string senderId { get; set; }      // User who sent the friend request
    public User sender { get; set; }

    public string receiverId { get; set; }    // User who received the friend request
    public User receiver { get; set; }

    public DateTime SentAt { get; set; }
    public bool IsAccepted { get; set; }
    public DateTime? AcceptedAt { get; set; } 
}