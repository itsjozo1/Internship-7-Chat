namespace Chat.Data.Entities;

public class User
{
    public User(string email, string password, bool isAdmin)
    {
        Email = email;
        Password = password;
        IsAdmin = isAdmin;
    }

    public User()
    {
        
    }
    public bool IsAdmin { get; set; }
    public int UserId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();
    public ICollection<GroupMessage> GroupMessages { get; set; } = new List<GroupMessage>();
    public ICollection<PrivateMessage> SentMessages { get; set; } = new List<PrivateMessage>();
    public ICollection<PrivateMessage> ReceivedMessages { get; set; } = new List<PrivateMessage>();

    public string TrimUserName()
    {
        int atIndex = Email.IndexOf('@');
        if (atIndex != -1)
        {
            string trimmedUsername = Email.Substring(0, atIndex);
            return trimmedUsername;
        }

        return null;
    }
}