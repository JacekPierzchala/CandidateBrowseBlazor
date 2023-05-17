namespace CandidateBrowserCleanArch.Blazor.WASM.Statics;

internal static class PermissionStatics
{
    internal const string CandidateCreate = "Candidate.Create";
    internal const string CandidateDelete = "Candidate.Delete";
    internal const string CandidateRead = "Candidate.Read";
    internal const string CandidateUpdate = "Candidate.Update";
}

internal static class CustomClaimTypes
{
    internal const string Permission = "Permission";
}

internal static class UrlStatics
{
    internal const string _localAPIHostUrl = "https://localhost:7201/";
    internal const string _azureAPIHostUrl = "https://candidatebrowsercleanarchapi.azurewebsites.net/";
    internal const string home = "";
    internal const string login = "/users/login";
    internal const string register = "/users/register";
    internal const string logout = "/users/logout";
    internal const string externalAuthentication = "/users/externalAuthentication";
    internal const string resendConfirmation = "/users/resendConfirmation";
    internal const string forgotPassword = "/users/forgotpassword";
    internal const string resetpassword = "/users/resetpassword";
    internal const string signinGoogle = "signin-google";  
    internal static string[] externalAuthEndpoints = new string[] { "signin-google?" };
}
