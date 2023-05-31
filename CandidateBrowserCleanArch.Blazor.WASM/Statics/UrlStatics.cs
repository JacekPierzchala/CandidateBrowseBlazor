namespace CandidateBrowserCleanArch.Blazor.WASM.Statics;

internal static class UrlStatics
{
    internal const string _localAPIHostUrl = "https://localhost:7201/";
    internal const string _azureAPIHostUrl = "https://candidatebrowsercleanarchapi.azurewebsites.net/";
    internal const string home = "";
    internal const string login = "/auth/login";
    internal const string register = "/auth/register";
    internal const string logout = "/auth/logout";
    internal const string users = "/users";
    internal const string externalAuthentication = "/auth/externalAuthentication";
    internal const string resendConfirmation = "/auth/resendConfirmation";
    internal const string forgotPassword = "/auth/forgotpassword";
    internal const string resetpassword = "/auth/resetpassword";
    internal const string signinGoogle = "signin-google";  
    internal static string[] externalAuthEndpoints = new string[] { "signin-google?" };
}
