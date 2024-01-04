
using Chat.Data.Entities;


namespace Chat.Domain.Repositories;

public class GroupRepository : Base
{
    public GroupRepository(ChatDbContext dbContext) : base(dbContext)
    {
    }

    public ResponseResultType Add(Group group)
    {
        DbContext.Groups.Add(group);

        return SaveChanges();
    }

    public Group? GetById(int id) => DbContext.Groups.FirstOrDefault(g => g.GroupId == id);

    public Group? GetByName(string name) => DbContext.Groups.FirstOrDefault(g => g.Name == name);

    public List<Group> GetAll() => DbContext.Groups.ToList();

}