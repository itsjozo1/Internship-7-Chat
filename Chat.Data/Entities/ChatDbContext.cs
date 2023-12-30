using Chat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;


namespace Chat.Data;

public class ChatDbContext : DbContext
{
    public ChatDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Group> Groups => Set<Group>();
    public DbSet<User> Users => Set<User>();
    public DbSet<GroupMessage> GroupMessages => Set<GroupMessage>();
    public DbSet<GroupUser> GroupUsers => Set<GroupUser>();
    public DbSet<PrivateMessage> PrivateMessages => Set<PrivateMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GroupUser>()
            .HasKey(gu => new { gu.UserId, gu.GroupId });

        modelBuilder.Entity<GroupUser>()
            .HasOne(g => g.User)
            .WithMany(u => u.GroupUsers)
            .HasForeignKey(gu => gu.UserId);

        modelBuilder.Entity<GroupUser>()
            .HasOne(g => g.Group)
            .WithMany(g => g.GroupUsers)
            .HasForeignKey(gu => gu.GroupId);

        modelBuilder.Entity<GroupMessage>()
            .HasKey(gm => gm.Id);

        modelBuilder.Entity<GroupMessage>()
            .HasOne(u => u.User)
            .WithMany(gm => gm.GroupMessages)
            .HasForeignKey(gm => gm.UserId);

        modelBuilder.Entity<GroupMessage>()
            .HasOne(g => g.Group)
            .WithMany(m => m.GroupMessagesCollection)
            .HasForeignKey(gm => gm.GroupId);

        modelBuilder.Entity<PrivateMessage>()
            .HasKey(pm => pm.Id);

        modelBuilder.Entity<PrivateMessage>()
            .HasOne(su => su.SentUser)
            .WithMany(pm => pm.SentMessages)
            .HasForeignKey(pm => pm.SentUserId);

        modelBuilder.Entity<PrivateMessage>()
            .HasOne(ru => ru.RecievedUser)
            .WithMany(pm => pm.ReceivedMessages)
            .HasForeignKey(pm => pm.RecievedUserId);

        base.OnModelCreating(modelBuilder);
    }


 
    public class ChatDbContextFactory : IDesignTimeDbContextFactory<ChatDbContext>
    {
        public ChatDbContext CreateDbContext(string[] args)
        {
            var configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configPath)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ChatDbContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("Chat"));

            return new ChatDbContext(optionsBuilder.Options);
        }
    }
 
}

