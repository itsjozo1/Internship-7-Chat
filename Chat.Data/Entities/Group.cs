namespace Chat.Data.Entities;

public class Group
{
    public Group(string name)
    {
        Name = name;
    }
    public Group()
    {
        
    }
    public int GroupId { get; set; }
    public string Name { get; set; }
    
    public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();
    public ICollection<GroupMessage> GroupMessagesCollection { get; set; } = new List<GroupMessage>();
}