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

    public ICollection<PrivateMessage> GetAll() => DbContext.PrivateMessages.ToList();
    
    public List<User> GetAllUsersPrivateChats(int userId)
    {
        var senderIds = DbContext.PrivateMessages
            .Where(pm => pm.SentUserId == userId)
            .Select(pm => pm.RecievedUserId)
            .Distinct()
            .ToList();

        var receiverIds = DbContext.PrivateMessages
            .Where(pm => pm.RecievedUserId == userId)
            .Select(pm => pm.SentUserId)
            .Distinct()
            .ToList();

        var userIDs = senderIds.Union(receiverIds).Distinct().ToList();

        var usersWithMessages = DbContext.Users
            .Where(u => userIDs.Contains(u.UserId))
            .ToList();

        return usersWithMessages;
    }
    
    public List<PrivateMessage> GetAllChatMessages(int userId1, int userId2)
    {
        var messages = DbContext.PrivateMessages
            .Where(pm =>
                (pm.SentUserId == userId1 && pm.RecievedUserId == userId2) ||
                (pm.SentUserId == userId2 && pm.RecievedUserId == userId1))
            .OrderBy(pm => pm.SentTime)
            .ToList();

        return messages;
    }
    public void DeletePrivateMessagesByUserId(int userId)
    {
        var messagesToDelete = DbContext.PrivateMessages
            .Where(pm => pm.SentUserId == userId || pm.RecievedUserId == userId)
            .ToList();

        DbContext.PrivateMessages.RemoveRange(messagesToDelete);
        DbContext.SaveChanges();
    }
}