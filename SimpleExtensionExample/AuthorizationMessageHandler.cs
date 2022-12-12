using System.Net.Http.Headers;

public class AuthorizationMessageHandler : HttpClientHandler
{
    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "5a8f1058-fc78-433e-b9a7-6aed21b70514");
        request.Headers.Add("api-version", "1");
        request.Headers.TryAddWithoutValidation("Content-Type", "application/fhir+json;fhirVersion=4.0");
        request.Headers.TryAddWithoutValidation("Accept", "application/fhir+json;fhirVersion=4.0");
        return await base.SendAsync(request, cancellationToken);
    }
}