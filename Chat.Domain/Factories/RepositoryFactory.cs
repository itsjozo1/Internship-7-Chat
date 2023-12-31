using Chat.Domain.Repositories;
using Microsoft.Extensions.Configuration;

namespace Chat.Domain.Factories
{
    public class RepositoryFactory
    {
        public static TRepository Create<TRepository>(IConfiguration configuration)
            where TRepository : Base
        {
            var dbContext = DbContextFactory.GetChatDbContext(configuration);
            var repositoryInstance = Activator.CreateInstance(typeof(TRepository), dbContext) as TRepository;

            return repositoryInstance!;
        }
    }
}
