using Chat.Data.Entities;

namespace Chat.Domain.Repositories;

public abstract class Base
{
    protected readonly ChatDbContext DbContext;

    protected Base(ChatDbContext dbContext)
    {
        DbContext = dbContext;
    }

    protected ResponseResultType SaveChanges()
    {
        var hasChanges = DbContext.SaveChanges() > 0;
        if (hasChanges)
            return ResponseResultType.Success;

        return ResponseResultType.NoChanges;
    }
}