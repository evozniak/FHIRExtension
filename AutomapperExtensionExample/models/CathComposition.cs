using Hl7.Fhir.Model;

public class CathCompositionExtensionStatus
{
    public string System { get; set; }
    public string Code { get; set; }
    public string Display { get; set; }
}

public class CathCompositionExtension
{
    public int? VersionNumber { get; set; }
    public CathCompositionExtensionStatus? Status { get; set; }
}

public class CathComposition : Composition
{
    public CathCompositionExtension CathExtension { get; set; }
}
