namespace Messenger.Models;

public class Files
{
    public int Id { get; set; }
    public string path { get; set; }
    public DateTime createdAt { get; set; }

    public User user { get; set; }
}