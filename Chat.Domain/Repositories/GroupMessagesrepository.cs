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

    public ResponseResultType Delete(int id)
    {
        var gMessageToDelete = DbContext.GroupMessages.Find(id);
        if (gMessageToDelete is null)
        {
            return ResponseResultType.NotFound;
        }

        DbContext.GroupMessages.Remove(gMessageToDelete);

        return SaveChanges();
    }

    public List<GroupMessage> GetAll() => DbContext.GroupMessages.ToList();
    
    public List<GroupMessage> GetAllSortedBySentTime(int groupId)
    {
        return DbContext.GroupMessages
            .Where(message => message.GroupId == groupId)
            .OrderBy(message => message.SentTime)
            .ToList();
    }
}