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

    public ICollection<GroupMessage> GetAll() => DbContext.GroupMessages.ToList();
}