namespace CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base
{
    public partial interface ICandidateBrowserWebAPIClient
    {
        public HttpClient HttpClient { get; }
    }
}
