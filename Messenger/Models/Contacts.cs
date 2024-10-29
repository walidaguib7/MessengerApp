namespace Messenger.Models;

public class Contacts
{
    public string userId { get; set; }        // The user who has the friend/contact
    public User user { get; set; }
    
    public string friendId { get; set; }      // The friend/contact of the user
    public User friend { get; set; }

    public DateTime AddedAt { get; set; } = DateTime.Today;
}