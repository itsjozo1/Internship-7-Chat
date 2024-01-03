namespace Chat.Data.Entities;

public class GroupUser
{
    public int GroupUserId { get; set; }
    public int GroupId { get; set; }
    public Group Group { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}