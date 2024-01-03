using Chat.Data.Entities;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Chat.Helper;

namespace Chat.Actions;

public class Login
{
    private const int MaxAttempts = 3;
    private const int CooldownTimeSeconds = 30;

    public static User? Create()
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
                Console.WriteLine($"Previše pokušaja, pokušajte ponovno za {roundedRemainingSeconds} sekundi.");
                Thread.Sleep(1000);

                if (DateTime.Now >= cooldownEndTime)
                {
                    cooldownActive = false;
                }
                Console.Clear();
            }

            Console.WriteLine("Unesite svoj email: ");
            var email = Console.ReadLine();
            Console.WriteLine("Unesite lozinku: ");
            var password = Console.ReadLine();

            var user = users.GetByEmail(email);

            if (user != null && user.Password == password)
            {
                Console.WriteLine("Uspiješna prijava!");
                return user;
            }

            attempts++;

            if (attempts >= MaxAttempts)
            {
                cooldownActive = true;
                attempts = 0;
                cooldownEndTime = DateTime.Now.AddSeconds(CooldownTimeSeconds);
            }

            Console.Clear();
            Console.WriteLine("Netočan email ili lozinka, pokušajte ponovno: .");
        }
    }
}