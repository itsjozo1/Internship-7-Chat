using Chat.Data.Entities;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Chat.Helper;
using System;
using Chat.Domain;

namespace Chat.Actions
{
    public class ChangePassword
    {
        public static void Create(User user)
        {
            UserRepository userRepository = RepositoryFactory.Create<UserRepository>(ConfigHelper.GetConfig());
            Console.Clear();
            string newPassword;
            ConfirmPassword(user.Password);
            do
            {
                newPassword = GetNewPassword();
            } while (newPassword == user.Password);
            ConfirmPassword(newPassword);

            var changePassword = userRepository.Update(new User(user.Email, newPassword, user.IsAdmin),
                user.UserId);
            if (changePassword != ResponseResultType.Success)
            {
                Console.WriteLine($"Pogreška: {changePassword}, za nastavak pritisnite enter: ");
                Console.ReadKey();
                return;
            }
            Console.Clear();
            Console.WriteLine("Uspiješno promjenjena lozinka, za nastavak pritisnite enter: ");
            Console.ReadKey();
        }

        static string GetNewPassword()
        {
            string password;
            do
            {
                Console.WriteLine("Unesite novu lozinku (minimalno 6 znakova): ");
                password = Console.ReadLine();
                if (string.IsNullOrEmpty(password) || password.Length < 6)
                {
                    Console.WriteLine("Nova lozinka mora sadržavati najmanje 6 znakova.");
                }
            } while (string.IsNullOrEmpty(password) || password.Length < 6);

            return password;
        }

        static void ConfirmPassword(string newPassword)
        {
            string reenteredPassword;
            do
            {
                Console.WriteLine("Potvrdite lozinku: ");
                reenteredPassword = Console.ReadLine();

                if (reenteredPassword != newPassword)
                {
                    Console.WriteLine("Lozinke se ne podudaraju, unesite ponovno: ");
                }
            } while (reenteredPassword != newPassword);
        }
    }
}
