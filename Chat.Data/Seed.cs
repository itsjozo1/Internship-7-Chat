using Bogus;
using Chat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Chat.Data
{
    public static class Seed
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            int groupId = 1;
            int userId = 1;
            int groupUserId = 1;
            int groupMessageId = 1;
            int privateMessageId = 1;

            var groupFaker = new Faker<Group>()
                .RuleFor(g => g.GroupId, f => groupId++)
                .RuleFor(g => g.Name, f => f.Commerce.Department());

            modelBuilder.Entity<Group>().HasData(groupFaker.Generate(10));

            var userFaker = new Faker<User>()
                .RuleFor(u => u.UserId, f => userId++)
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.IsAdmin, f => f.Random.Bool())
                .RuleFor(u => u.Password, f => f.Internet.Password());

            modelBuilder.Entity<User>().HasData(userFaker.Generate(20));

            var groupUserFaker = new Faker<GroupUser>()
                .RuleFor(gu => gu.GroupUserId, f => groupUserId++)
                .RuleFor(gu => gu.GroupId, f => f.Random.Number(1, 10))
                .RuleFor(gu => gu.UserId, f => f.Random.Number(1, 20));

            var groupUsers = groupUserFaker.Generate(60)
                .DistinctBy(gu => new { gu.UserId, gu.GroupId })
                .ToList();

            modelBuilder.Entity<GroupUser>().HasData(groupUsers);

            var groupMessageFaker = new Faker<GroupMessage>()
                .RuleFor(gm => gm.GroupMessageId, f => groupMessageId++)
                .RuleFor(gm => gm.SentTime, f => f.Date.Recent().ToUniversalTime())
                .RuleFor(gm => gm.Content, f => f.Lorem.Sentence());

            var groupMessages = Enumerable.Range(0, 150).Select(_ =>
            {
                var randomGroupUserIndex = new Faker().Random.Int(0, groupUsers.Count - 1);
                var randomGroupUser = groupUsers[randomGroupUserIndex];
                var userGroup = GetUserAndGroupId(randomGroupUser.GroupUserId, groupUsers);
                return groupMessageFaker
                    .RuleFor(gm => gm.GroupId, f => userGroup.GroupId)
                    .RuleFor(gm => gm.UserId, f => userGroup.UserId)
                    .Generate();
            }).ToList();

            modelBuilder.Entity<GroupMessage>().HasData(groupMessages);


            var privateMessageFaker = new Faker<PrivateMessage>()
                .RuleFor(pm => pm.PrivateMessageId, f => privateMessageId++)
                .RuleFor(pm => pm.SentTime, f => f.Date.Recent().ToUniversalTime())
                .RuleFor(pm => pm.Content, f => f.Lorem.Sentence())
                .RuleFor(pm => pm.SentUserId, f => f.Random.Number(1, 20))
                .RuleFor(pm => pm.RecievedUserId, f => f.Random.Number(1, 20));

            modelBuilder.Entity<PrivateMessage>().HasData(privateMessageFaker.Generate(100));
        }

        static (int UserId, int GroupId) GetUserAndGroupId(int groupUserId, List<GroupUser> groupUsers)
        {
            var userGroup = groupUsers.FirstOrDefault(gu => gu.GroupUserId == groupUserId);
            if (userGroup != null)
            {
                return (userGroup.UserId, userGroup.GroupId);
            }
            return (0, 0);
        }
    }
}
