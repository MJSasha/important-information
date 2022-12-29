using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace ImpInfFrontCommon.Utils
{
    public class IncludeRequestCredentialsMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine("вызван IncludeRequestCredentialsMessageHandler");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
