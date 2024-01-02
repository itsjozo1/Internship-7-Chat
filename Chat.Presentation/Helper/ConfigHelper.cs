using Microsoft.Extensions.Configuration;

namespace Chat.Helper;

public class ConfigHelper
{
    public static IConfigurationRoot GetConfig()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("/Users/rokoperisic/Internship-7-Chat/Chat.Data/appsettings.json") 
            .Build();
        return config;
    }
}