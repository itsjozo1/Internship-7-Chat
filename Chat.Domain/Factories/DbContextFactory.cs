using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Chat.Data.Entities;

namespace Chat.Domain.Factories
{
    public static class DbContextFactory
    {
        public static ChatDbContext GetChatDbContext(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ChatDbContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("Chat"));

            return new ChatDbContext(optionsBuilder.Options);
        }
    }
}
