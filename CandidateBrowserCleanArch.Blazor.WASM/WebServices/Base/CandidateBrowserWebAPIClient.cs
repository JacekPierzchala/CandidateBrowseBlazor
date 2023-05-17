namespace CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base
{
    public partial class CandidateBrowserWebAPIClient : ICandidateBrowserWebAPIClient
    {
        public HttpClient HttpClient => _httpClient;
    }
}
