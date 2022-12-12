using AutoMapper;
using Hl7.Fhir.Model;

public class ExtensionTypeConverter : ITypeConverter<List<Extension>, CathCompositionExtension>
{
    const string VERSION_EXTENSION_URL = "http://hl7.org/fhir/StructureDefinition/composition-clinicaldocument-versionNumber";
    const string STATUS_EXTENSION_URL = "https://www.fhir.philips.com/4.0/StructureDefinition/hds/composition/composition-v1/HDSCompositionStatusExtension";
    public CathCompositionExtension Convert(List<Extension> source, CathCompositionExtension destination, ResolutionContext context)
    {
        var cathExtension = new CathCompositionExtension();
        cathExtension.VersionNumber = int.Parse(source.Find(x => x.Url == VERSION_EXTENSION_URL).Value.ToString());
        cathExtension.Status = new CathCompositionExtensionStatus();
        var statusCodableConcept = source.Find(x => x.Url == STATUS_EXTENSION_URL).Value as CodeableConcept;
        cathExtension.Status.System = statusCodableConcept.Coding.First().System;
        cathExtension.Status.Display = statusCodableConcept.Coding.First().Display;
        cathExtension.Status.Code = statusCodableConcept.Coding.First().Code;
        return cathExtension;
    }
}