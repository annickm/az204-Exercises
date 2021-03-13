namespace AuthorizeWebApplication.Constants
{

    public class Clients
    {
        public static readonly Client Client1 = new Client
        {
            Id = "123456",
            Secret = "abcedf",
            RedirectUrl = Paths.AuthorizeCodeCallBackPath
        };
        public static readonly Client Client2 = new Client
        {
            Id = "78901",
            Secret = "aasdsdef",
            RedirectUrl = Paths.ImplicitGrantCallBackPath
        };

        public class Client
        {
            public  string Id { get; set; }
            public string Secret { get; set; }
            public string RedirectUrl { get; set; }
        }
    }
}