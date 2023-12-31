using Chat.Data.Entities;


namespace Chat.Domain.Repositories;

public class GroupUserRepository : Base
{
    public GroupUserRepository(ChatDbContext dbContext) : base(dbContext)
    {
    }

    public ResponseResultType Add(GroupUser groupUser)
    {
        DbContext.GroupUsers.Add(groupUser);

        return SaveChanges();
    }

    public ResponseResultType Delete(int id)
    {
        var groupUserToDelete = DbContext.GroupUsers.Find(id);
        if (groupUserToDelete is null)
        {
            return ResponseResultType.NotFound;
        }

        DbContext.GroupUsers.Remove(groupUserToDelete);

        return SaveChanges();
    }
}