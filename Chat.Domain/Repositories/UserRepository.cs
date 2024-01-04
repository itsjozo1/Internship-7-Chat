
using Chat.Data.Entities;

namespace Chat.Domain.Repositories;

public class UserRepository : Base
{
    public UserRepository(ChatDbContext dbContext) : base(dbContext)
    {
    }

    public ResponseResultType Add(User user)
    {
        DbContext.Users.Add(user);

        return SaveChanges();
    }

    public ResponseResultType Delete(int id)
    {
        var userToDelete = DbContext.Users.Find(id);
        if (userToDelete is null)
        {
            return ResponseResultType.NotFound;
        }

        DbContext.Users.Remove(userToDelete);

        return SaveChanges();
    }

    public ResponseResultType Update(User user, int id)
    {
        var userToUpdate = DbContext.Users.Find(id);
        if (userToUpdate is null)
        {
            return ResponseResultType.NotFound;
        }

        userToUpdate.Email = user.Email;
        userToUpdate.Password = user.Password;
        userToUpdate.IsAdmin = user.IsAdmin;

        return SaveChanges();
    }

    public User? GetById(int id) => DbContext.Users.FirstOrDefault(u => u.UserId == id);
    public User? GetByEmail(string email) => DbContext.Users.FirstOrDefault(u => u.Email == email);

    public ICollection<User> GetAll() => DbContext.Users.ToList();

    /*public ICollection<UserWithGroups> GetByGroupId(int groupId)
    {
        var users = DbContext.Users
            .Include(u => u.GroupUsers)
            .ThenInclude(gu => gu.Group)
            .Select(u => new UserWithGroups
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Groups = u.GroupUsers
                    .Select(gu => gu.Group)
                    .ToList(),
            })
            .Where(u => u.Groups.Any(g => g.Id == groupId))
            .ToList();

        return users;
    }*/
    public string TrimUsernameBeforeAtSign(int userId)
    {
        var user = DbContext.Users.FirstOrDefault(u => u.UserId == userId);
        if (user == null)
        {
            throw new InvalidOperationException("Korisnik nepostoji");
        }

        int atIndex = user.Email.IndexOf('@');
        if (atIndex != -1)
        {
            string trimmedUsername = user.Email.Substring(0, atIndex);
            return trimmedUsername;
        }

        throw new InvalidOperationException("Netočan email format");
    }
}