namespace AuthorizeWebApplication.Constants
{
    public class Paths
    {
        public const string AuthorizationServerBaseAddress = "https://localhost:44372";
        public const string ResourceServerBaseAddress = "https://localhost:44372";
        public const string ImplicitGrantCallBackPath = "https://localhost:44372/Home/SignIn";
        public const string AuthorizeCodeCallBackPath = "https://localhost:44372/Manage";
        public const string AuthorizePath = "/OAuth/Authorize";
        public const string TokenPath = "/OAuth/Token";
        public const string LoginPath = "Account/Login";
        public const string LogoutPath = "/Account/Logout";
        public const string MePath = "/api/Me";
    }
}