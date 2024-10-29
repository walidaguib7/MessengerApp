namespace Messenger.Models;

public class Members
{
    public string userId { get; set; }
    public User user { get; set; }

    public int conversationId { get; set; }
    public Conversation conversation { get; set; }

    public DateTime JoinedAt { get; set; } = DateTime.Today;
}