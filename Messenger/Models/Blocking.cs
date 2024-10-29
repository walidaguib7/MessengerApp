namespace Messenger.Models;

public class Blocking
{
    public string userId { get; set; }
    public User user { get; set; }
    public string blockedUserId { get; set; }
    public User blockedUser { get; set; }
}