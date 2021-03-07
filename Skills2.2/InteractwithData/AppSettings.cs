//C# .NET Core
using Microsoft.Extensions.Configuration;

namespace InteractwithData
{
    public class AppSettings
    {
        public string SourceSASConnectionString { get; set; }
        public string SourceAccountName { get; set; }
        public string SourceContainerName { get; set; }
        public string DestinationASAConnectionString { get; set; }
        public string DestinationAccountName { get; set; }
        public string DestinationContainerName { get; set; }

        public static AppSettings LoadAppSettings()
        {
            IConfigurationRoot configRoot = new ConfigurationBuilder()
                .AddJsonFile("AppSettings.json", false)
                .Build();
            AppSettings appSettings = configRoot.Get<AppSettings>();
            return appSettings;
        }
    }
}
