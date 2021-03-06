//C# .NET Core

using Microsoft.Extensions.Configuration;

namespace Skills2._2
{
    public class AppSettings
    {
        public string SASToken { get; set; }
        public string AccountName { get; set; }
        public string ContainerName { get; set; }

        public static AppSettings LoadAppSettings()
        {
            IConfiguration configRoot = new ConfigurationBuilder()
                .AddJsonFile("AppSettings.json", false)
                .Build();
                AppSettings appSettings = configRoot.Get<AppSettings>();
                return appSettings;
        }

    }
}