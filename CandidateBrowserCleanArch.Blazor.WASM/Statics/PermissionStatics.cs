namespace CandidateBrowserCleanArch.Blazor.WASM.Statics;

internal static class PermissionStatics
{
    internal const string CandidateCreate = "Candidate.Create";
    internal const string CandidateDelete = "Candidate.Delete";
    internal const string CandidateRead = "Candidate.Read";
    internal const string CandidateUpdate = "Candidate.Update";

    internal const string UserAssignRole = "User.AssignRole";
    internal const string UserDelete = "User.Delete";
    internal const string UserLock = "User.Lock";
    internal const string UserUpdate = "User.Update";
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
