
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
    
    public User? GetByEmail(string email) => DbContext.Users.FirstOrDefault(u => u.Email == email);

    public ICollection<User> GetAll() => DbContext.Users.ToList();


}