using Chat.Data.Entities;

namespace Chat.Domain.Repositories;

public class PrivateMessageRepository : Base
{
    public PrivateMessageRepository(ChatDbContext dbContext) : base(dbContext)
    {
        
    }
    public ResponseResultType Add(PrivateMessage privateMessage)
    {
        DbContext.PrivateMessages.Add(privateMessage);

        return SaveChanges();
    }

    public ResponseResultType Delete(int id)
    {
        var pMessageToDelete = DbContext.PrivateMessages.Find(id);
        if (pMessageToDelete is null)
        {
            return ResponseResultType.NotFound;
        }

        DbContext.PrivateMessages.Remove(pMessageToDelete);

        return SaveChanges();
    }
}