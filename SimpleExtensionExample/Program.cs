using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System;

const string VERSION_EXTENSION_URL = "http://hl7.org/fhir/StructureDefinition/composition-clinicaldocument-versionNumber";
const string STATUS_EXTENSION_URL = "https://www.fhir.philips.com/4.0/StructureDefinition/hds/composition/composition-v1/HDSCompositionStatusExtension";

Console.WriteLine("Hello, World!");

var baseUrl = "https://cdr-edisa-test.us-east.philips-healthsuite.com/store/fhir/75dffb1b-66c1-4a41-9232-36124d8fd707";
var settings = new FhirClientSettings
{
    PreferredFormat = ResourceFormat.Json,
};

var client = new FhirClient(baseUrl, settings, new AuthorizationMessageHandler());

var composition = client.Read<Composition>("Composition/a00cedee-3b4c-11ed-8544-8b4b4317be89");
Console.WriteLine("Composition status: " + composition.Status);
var versionNumber = composition.Extension.Find(x => x.Url == VERSION_EXTENSION_URL)?.Value;
CodeableConcept? statusExtension = composition.Extension.Find(x => x.Url == STATUS_EXTENSION_URL)?.Value as CodeableConcept;
var statusSystem = statusExtension.Coding.First().System;
var statusCode = statusExtension.Coding.First().Code;
var statusDisplay = statusExtension.Coding.First().Display;
Console.WriteLine("statusSystem: " + statusSystem);
Console.WriteLine("statusCode: " + statusCode);
Console.WriteLine("statusDisplay: " + statusDisplay);

//to Add a new extension
//option 1
composition.AddExtension("http://hl7.org/fhir/StructureDefinition/composition-clinicaldocument-versionNumber", new FhirString("1"));
//option 2
var versionNumberExtension = new Extension();
versionNumberExtension.Url = "http://hl7.org/fhir/StructureDefinition/composition-clinicaldocument-versionNumber";
versionNumberExtension.Value = new FhirString("1");
composition.Extension.Add(versionNumberExtension);