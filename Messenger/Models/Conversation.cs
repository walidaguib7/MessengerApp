namespace Messenger.Models;

public class Conversation
{
    public int Id { get; set; }
    public bool IsGroup { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Today;
    public List<Members> members { get; set; } = [];
    public List<Messages> Messages { get; set; } = [];
}