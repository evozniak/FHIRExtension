using AutoMapper;
using Hl7.Fhir.Model;

public class ReverseExtensionTypeConverter : ITypeConverter<CathCompositionExtension, List<Extension>>
{
    const string VERSION_EXTENSION_URL = "http://hl7.org/fhir/StructureDefinition/composition-clinicaldocument-versionNumber";
    const string STATUS_EXTENSION_URL = "https://www.fhir.philips.com/4.0/StructureDefinition/hds/composition/composition-v1/HDSCompositionStatusExtension";
    public List<Extension> Convert(CathCompositionExtension source, List<Extension> destination, ResolutionContext context)
    {
        var extensions = new List<Extension>();
        extensions.Add(new Extension(VERSION_EXTENSION_URL, new FhirString(source.VersionNumber.ToString())));

        if (source.Status is not null)
        {
            var statusCodableConcept = new CodeableConcept(source.Status.System, source.Status.Code, source.Status.Display, "");
            extensions.Add(new Extension(STATUS_EXTENSION_URL, statusCodableConcept));
        }

        return extensions;
    }
}