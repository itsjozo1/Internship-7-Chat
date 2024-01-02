using System;
using System.Threading;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Chat.Helper;

public class Login
{
    private const int MaxAttempts = 3;
    private const int CooldownTimeSeconds = 30;

    public static void Create()
    {
        UserRepository users = RepositoryFactory.Create<UserRepository>(ConfigHelper.GetConfig());
        int attempts = 0;
        bool cooldownActive = false;
        DateTime cooldownEndTime = DateTime.MinValue;

        while (true)
        {
            while (cooldownActive)
            {
                TimeSpan remainingTime = cooldownEndTime - DateTime.Now;
                int roundedRemainingSeconds = (int)Math.Round(remainingTime.TotalSeconds);
                Console.WriteLine($"Too many failed attempts. Please try again after {roundedRemainingSeconds} seconds.");
                Thread.Sleep(1000);

                if (DateTime.Now >= cooldownEndTime)
                {
                    cooldownActive = false;
                }
                Console.Clear();
            }

            Console.WriteLine("Enter your email: ");
            var email = Console.ReadLine();
            Console.WriteLine("Enter your password: ");
            var password = Console.ReadLine();

            var user = users.GetByEmail(email);

            if (user != null && user.Password == password)
            {
                Console.WriteLine("Login successful!");
                break;
            }

            attempts++;

            if (attempts >= MaxAttempts)
            {
                cooldownActive = true;
                attempts = 0;
                cooldownEndTime = DateTime.Now.AddSeconds(CooldownTimeSeconds);
            }

            Console.Clear();
            Console.WriteLine("Incorrect email or password. Please try again.");
        }
    }
}