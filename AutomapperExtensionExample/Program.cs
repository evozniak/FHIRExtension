using AutoMapper;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

var baseUrl = "https://cdr-edisa-test.us-east.philips-healthsuite.com/store/fhir/75dffb1b-66c1-4a41-9232-36124d8fd707";
var resourceURL = "Composition/a00cedee-3b4c-11ed-8544-8b4b4317be89";
var versionNumberURL = "http://hl7.org/fhir/StructureDefinition/composition-clinicaldocument-versionNumber";

Console.WriteLine("Hello, from FHIR!");
AutoMapperUtils.ConfigureAutoMapper();
var settings = new FhirClientSettings
{
    PreferredFormat = ResourceFormat.Json,
};

// var client = new FhirClient(baseUrl, settings, new AuthorizationMessageHandler());
var client = new CustomFHIRClient(baseUrl, settings, new AuthorizationMessageHandler());

var cathComposition = client.Read<CathComposition>(resourceURL);

Console.WriteLine("\r\n-> AutoMapped classes <-");
Console.WriteLine("Id: " + cathComposition.Id);
Console.WriteLine("Status: " + cathComposition.Status);
Console.WriteLine("Author: " + cathComposition.Author.First().Reference); //CRUD operations (client.Read) can only return references and does not support include.
Console.WriteLine("VersionNumber: " + cathComposition.CathExtension.VersionNumber);
Console.WriteLine("System: " + cathComposition.CathExtension.Status.System);
Console.WriteLine("Code: " + cathComposition.CathExtension.Status.Code);
Console.WriteLine("Display: " + cathComposition.CathExtension.Status.Display);

//Changing the extended field in the resource
cathComposition.CathExtension.VersionNumber++;
try
{
    Composition composition = AutoMapperUtils.Mapper.Map<Composition>(cathComposition);
    //This will throw an exception, the application is expecting an extended type in this case, not the original resource.
    client.Update(composition);
}
catch (Exception e) { Console.WriteLine(e.Message); }
//The client will accept the custom object and will automap it again to the FHIR compatible resource.
client.Update<Composition>(cathComposition);

//Reverting the static typed extension to a FHIR resource.
Composition revertedComposition = AutoMapperUtils.Mapper.Map<Composition>(cathComposition);
var revertedVersionNumber = revertedComposition.Extension.Find(x => x.Url == versionNumberURL)?.Value;
Console.WriteLine("revertedVersionNumber: " + revertedVersionNumber);