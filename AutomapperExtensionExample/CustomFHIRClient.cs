using AutoMapper;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification;

class CustomFHIRClient : FhirClient
{
    public CustomFHIRClient(Uri endpoint, FhirClientSettings settings = null, HttpMessageHandler messageHandler = null, IStructureDefinitionSummaryProvider provider = null) : base(endpoint, settings, messageHandler, provider)
    {
    }

    public CustomFHIRClient(string endpoint, FhirClientSettings settings = null, HttpMessageHandler messageHandler = null, IStructureDefinitionSummaryProvider provider = null) : base(endpoint, settings, messageHandler, provider)
    {
    }

    public new TResource Update<TResource>(TResource resource, bool versionAware = false) where TResource : Resource
    {
        if (resource is Composition && !(resource is CathComposition))
        {
            throw new Exception("You cant post a native FHIR composition, please use the extended CathComposition.");
        }
        if (resource is CathComposition)
        {
            CathComposition cathComposition = resource as CathComposition;
            Composition composition = AutoMapperUtils.Mapper.Map<Composition>(cathComposition);
            return base.Update(composition as TResource, versionAware);
        }
        return base.Update(resource, versionAware);
    }

    public new TResource Read<TResource>(string location, string ifNoneMatch = null, DateTimeOffset? ifModifiedSince = null) where TResource : Resource
    {
        if (typeof(TResource) == typeof(CathComposition))
        {
            Composition composition = base.Read<Composition>(location, ifNoneMatch, ifModifiedSince);
            CathComposition cathComposition = AutoMapperUtils.Mapper.Map<CathComposition>(composition);
            return cathComposition as TResource;
        }
        return base.Read<TResource>(location, ifNoneMatch, ifModifiedSince);
    }
}