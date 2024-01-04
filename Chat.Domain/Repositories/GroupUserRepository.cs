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
    
    public List<GroupUser> GetAllGroupUsersByUserId(int userId)
    {
        return DbContext.GroupUsers
            .Where(gu => gu.UserId == userId)
            .ToList();
    }
    public int CountUsersInGroup(int groupId)
    {
        return DbContext.GroupUsers
            .Count(gu => gu.GroupId == groupId);
    }
    public ResponseResultType DeleteByUserId(int userId)
    {
        var groupUserToDelete = DbContext.GroupUsers
            .FirstOrDefault(gu => gu.UserId == userId);

        if (groupUserToDelete == null)
        {
            return ResponseResultType.NotFound;
        }

        DbContext.GroupUsers.Remove(groupUserToDelete);
        DbContext.SaveChanges();

        return ResponseResultType.Success;
    }
}