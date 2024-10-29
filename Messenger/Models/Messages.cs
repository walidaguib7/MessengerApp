namespace Messenger.Models;

public class Messages
{
    public int Id { get; set; }
    public int ConversationId { get; set; }
    public Conversation Conversation { get; set; }

    public string senderId { get; set; }
    public User sender { get; set; }
    
    public string? receiverId { get; set; }    
    public User? receiver { get; set; }

    public string Content { get; set; }
    public DateTime SentAt { get; set; }
    
    public int? fileId { get; set; }
    public Files? file { get; set; }
    
    public bool IsRead { get; set; }
}