using Bogus;
using Chat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
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
                .RuleFor(u => u.Password, f => f.Internet.Password());

            modelBuilder.Entity<User>().HasData(userFaker.Generate(20));

            var groupUserFaker = new Faker<GroupUser>()
                .RuleFor(gu => gu.GroupUserId, f => groupUserId++)
                .RuleFor(gu => gu.GroupId, f => f.Random.Number(1, 10))
                .RuleFor(gu => gu.UserId, f => f.Random.Number(1, 20))
                .RuleFor(gu => gu.IsAdmin, f => f.Random.Bool());

            var groupUsers = groupUserFaker.Generate(60)
                .DistinctBy(gu => new { gu.UserId, gu.GroupId })
                .ToList();

            modelBuilder.Entity<GroupUser>().HasData(groupUsers);

            var groupMessageFaker = new Faker<GroupMessage>()
                .RuleFor(gm => gm.GroupMessageId, f => groupMessageId++)
                .RuleFor(gm => gm.SentTime, f => f.Date.Recent().ToUniversalTime())
                .RuleFor(gm => gm.Content, f => f.Lorem.Sentence());

            // Generate group messages for users in valid groups
            var groupMessages = groupUsers
                .Select(gu => new { gu.UserId, gu.GroupId })
                .Select(gu =>
                {
                    var groupIdForUser = gu.GroupId;
                    var userIdInGroup = gu.UserId;

                    var existingGroupIds = groupUsers.Select(g => g.GroupId).Distinct().ToList();

                    if (existingGroupIds.Contains(groupIdForUser)) // Check if the group exists
                    {
                        return groupMessageFaker
                            .RuleFor(gm => gm.GroupId, f => groupIdForUser)
                            .RuleFor(gm => gm.UserId, f => userIdInGroup)
                            .Generate();
                    }
                    return null;
                })
                .Where(gm => gm != null)
                .Take(150) // Take exactly 150 messages
                .ToList();

            int remainingMessagesCount = 150 - groupMessages.Count;

            if (remainingMessagesCount > 0)
            {
                var additionalMessages = groupMessageFaker.Generate(remainingMessagesCount);
                groupMessages.AddRange(additionalMessages);
            }

            modelBuilder.Entity<GroupMessage>().HasData(groupMessages);

            var privateMessageFaker = new Faker<PrivateMessage>()
                .RuleFor(pm => pm.PrivateMessageId, f => privateMessageId++)
                .RuleFor(pm => pm.SentTime, f => f.Date.Recent().ToUniversalTime())
                .RuleFor(pm => pm.Content, f => f.Lorem.Sentence())
                .RuleFor(pm => pm.SentUserId, f => f.Random.Number(1, 20))
                .RuleFor(pm => pm.RecievedUserId, f => f.Random.Number(1, 20));

            modelBuilder.Entity<PrivateMessage>().HasData(privateMessageFaker.Generate(100));
        }
    }
}
