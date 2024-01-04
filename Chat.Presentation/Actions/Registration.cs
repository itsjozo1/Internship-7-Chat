using System.Text;
using System.Text.RegularExpressions;
using Chat.Data.Entities;
using Chat.Domain;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Chat.Helper;
using Chat.Menus;

namespace Chat.Actions;

public class Registration : Menu
{
    public static User? Create()
    {
        UserRepository users = RepositoryFactory.Create<UserRepository>(ConfigHelper.GetConfig());
        
        string email = GetUserEmail(users);
        string password = GetPassword();
        ConfirmPassword(password);

        string captcha = GenerateCaptcha();
        ValidateCaptcha(captcha);
        
        if (users.Add(new User(email, password, false)) == ResponseResultType.Success)
        {
            Console.WriteLine("Uspješno ste registrirani.");
            return new User(email, password, false);
        }

        return null;
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
                Console.WriteLine("Korisnik sa uneseninm emailom već postoji, unesite ponovno: .");
            }
        } while (!IsValidEmail(email) || users.GetByEmail(email) != null);

        return email;
    }

    static bool IsValidEmail(string email)
    {
        string pattern = @"^[^\s@]+@[^\s@]+\.[^\s@]{3,}$";
        return Regex.IsMatch(email, pattern);
    }
    static string GetPassword()
    {
        string password;
        do
        {
            Console.WriteLine("Unesite lozinku (minimalno 6 znakova): ");
            password = IFunctionHelper.GetMaskedPassword();
            if (string.IsNullOrEmpty(password) || password.Length < 6)
            {
                Console.WriteLine("Lozinka mora sadržavati najmanje 6 znakova.");
            }
        } while (string.IsNullOrEmpty(password) || password.Length < 6);

        return password;
    }

    static void ConfirmPassword(string password)
    {
        string reenteredPassword;
        do
        {
            Console.WriteLine("Potvrdite lozinku: ");
            reenteredPassword = IFunctionHelper.GetMaskedPassword();
            
            if (reenteredPassword != password)
            {
                Console.WriteLine("Lozinke se ne podudaraju, unesite ponovno: ");
            }
        } while (reenteredPassword != password);
    }

    
    static string GenerateCaptcha()
    {
        string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; 
        Random random = new Random();
        StringBuilder captcha = new StringBuilder();
        bool hasNumber = false;
        bool hasCharacter = false;

        while (!hasNumber || !hasCharacter)
        {
            captcha = new StringBuilder("");
            for (int i = 0; i < 7; i++)
            {
                char ch = validChars[random.Next(validChars.Length)];
                captcha.Append(ch);

                if (char.IsDigit(ch))
                    hasNumber = true;
                else if (char.IsLetter(ch))
                    hasCharacter = true;
            }
        }

        return captcha.ToString();
    }
    static void ValidateCaptcha(string captcha)
    {
        string userInput;
        do
        {
            Console.Clear();
            Console.WriteLine("Unesite znakove prikazane na slici: :");
            Console.WriteLine(captcha);
            userInput = Console.ReadLine();

            if (userInput != captcha)
            {
                Console.WriteLine("Netočna CAPTCHA, nesite ponovno.");
                Console.ReadKey(); 
            }
        } while (userInput != captcha);
    }
}