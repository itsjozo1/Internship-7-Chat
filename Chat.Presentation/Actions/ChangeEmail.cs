using System.Text.RegularExpressions;
using Chat.Data.Entities;
using Chat.Domain;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Chat.Helper;
using Chat.Menus;

namespace Chat.Actions;

public class ChangeEmail
{
    public static void Create(User user)
    {
        UserRepository userRepository = RepositoryFactory.Create<UserRepository>(ConfigHelper.GetConfig());
        Console.WriteLine($"Trenutni email korisnika je {user.Email}\nUnesite novi email: ");
        var newEmail = GetUserEmail(userRepository);
        Console.Clear();
        Console.WriteLine($"Želite li proijeniti email korisnika iz {user.Email}\n" +
                          $"u {newEmail} (da/ne): ");
        if (Menu.Confirm())
        {
            var updateUser = userRepository.Update(new User(newEmail, user.Password, user.IsAdmin), user.UserId);
            if (updateUser != ResponseResultType.Success)
            {
                Console.Clear();
                Console.WriteLine($"Pogreška: {updateUser}\nZa nastavak pritisnite enter");
                Console.ReadKey();
                return;
            }
            Console.Clear();
            Console.WriteLine("Uspiješna promjena, za nastavak pritisnite enter: ");
            Console.ReadKey();
        }
    }
    static string GetUserEmail(UserRepository users)
    {
        string email;
        do
        {
            Console.WriteLine("Unesite email: ");
            email = Console.ReadLine();
            
            if (!IsValidEmail(email))
            {
                Console.WriteLine("Netočan format emaila, unesite ponovno: .");
            }
            else if (users.GetByEmail(email) != null)
            {
                Console.WriteLine("Korisnik sa unesenim emailom već postoji, unesite ponovno: .");
            }
        } while (!IsValidEmail(email) || users.GetByEmail(email) != null);

        return email;
    }

    static bool IsValidEmail(string email)
    {
        string pattern = @"^[^\s@]+@[^\s@]+\.[^\s@]{3,}$";
        return Regex.IsMatch(email, pattern);
    }
}