using AutoMapper;
using Hl7.Fhir.Model;

class AutoMapperUtils
{
    public static Mapper Mapper { get; set; }

    public static void ConfigureAutoMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<List<Extension>, CathCompositionExtension>().ConvertUsing(new ExtensionTypeConverter());
            cfg.CreateMap<CathCompositionExtension, List<Extension>>().ConvertUsing(new ReverseExtensionTypeConverter());
            cfg.CreateMap<Composition, CathComposition>()
            //Manual mapping, is better to use custom Converters ;)
            // .ForPath(dst => dst.CathExtension.VersionNumber, map => map.MapFrom(src => (src.Extension.Find(x => x.Url == VERSION_EXTENSION_URL).Value).ToString()))
            // .ForPath(dst => dst.CathExtension.Status.System, map => map.MapFrom(src => (src.Extension.Find(x => x.Url == STATUS_EXTENSION_URL).Value as CodeableConcept).Coding.First().System))
            // .ForPath(dst => dst.CathExtension.Status.Code, map => map.MapFrom(src => (src.Extension.Find(x => x.Url == STATUS_EXTENSION_URL).Value as CodeableConcept).Coding.First().Code))
            // .ForPath(dst => dst.CathExtension.Status.Display, map => map.MapFrom(src => (src.Extension.Find(x => x.Url == STATUS_EXTENSION_URL).Value as CodeableConcept).Coding.First().Display))
            .ForMember(dst => dst.CathExtension, map => map.MapFrom(src => src.Extension))
            .IgnoreAllPropertiesWithAnInaccessibleSetter()
            .ReverseMap()
            .ForMember(ori => ori.Extension, map => map.MapFrom(dst => dst.CathExtension))
            .IgnoreAllPropertiesWithAnInaccessibleSetter();
        });

        config.AssertConfigurationIsValid();
        AutoMapperUtils.Mapper = new Mapper(config);
    }
}