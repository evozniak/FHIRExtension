using System.Net.Http.Headers;

public class AuthorizationMessageHandler : HttpClientHandler
{
    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "32b0cab7-50b9-4048-af3d-27d4327aab9a");
        request.Headers.Add("api-version", "1");
        request.Headers.Add("X-validate-resource", "false");
        request.Headers.TryAddWithoutValidation("Content-Type", "application/fhir+json;fhirVersion=4.0");
        request.Headers.TryAddWithoutValidation("Accept", "application/fhir+json;fhirVersion=4.0");
        return await base.SendAsync(request, cancellationToken);
    }
}