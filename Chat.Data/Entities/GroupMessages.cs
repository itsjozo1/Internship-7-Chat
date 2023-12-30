namespace Chat.Data.Entities;

public class GroupMessage
{
    public int Id { get; set; }
    public DateTime SentTime { get; set; }
    public string Content { get; set; }
    
    public int GroupId { get; set; }
    public Group? Group { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}