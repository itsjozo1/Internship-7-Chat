using Chat.Data.Entities;

namespace Chat.Domain.Repositories;

public class GroupMessagesrepository : Base
{
    public GroupMessagesrepository(ChatDbContext dbContext) : base(dbContext)
    {
        
    }
    public ResponseResultType Add(GroupMessage groupMessage)
    {
        DbContext.GroupMessages.Add(groupMessage);

        return SaveChanges();
    }
    
    public List<GroupMessage> GetAllSortedBySentTime(int groupId)
    {
        return DbContext.GroupMessages
            .Where(message => message.GroupId == groupId)
            .OrderBy(message => message.SentTime)
            .ToList();
    }
    public void DeleteGroupMessagesByUserId(int userId)
    {
        var messagesToDelete = DbContext.GroupMessages.Where(gm => gm.UserId == userId).ToList();

        foreach (var message in messagesToDelete)
        {
            DbContext.GroupMessages.Remove(message);
        }

        DbContext.SaveChanges();
    }
}