namespace Chat.Data.Entities;

public class User
{
    public User(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public User()
    {
        
    }
    public int UserId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();
    public ICollection<GroupMessage> GroupMessages { get; set; } = new List<GroupMessage>();
    public ICollection<PrivateMessage> SentMessages { get; set; } = new List<PrivateMessage>();
    public ICollection<PrivateMessage> ReceivedMessages { get; set; } = new List<PrivateMessage>();
}