
namespace Microsoft.OpenApi
{
    internal class Models
    {
        public static object SecuritySchemeType { get; internal set; }
        public static object ParameterLocation { get; internal set; }
        public static object ReferenceType { get; internal set; }

        internal class OpenApiSecurityScheme : IOpenApiSecurityScheme
        {
            public string Name { get; set; }
            public object Type { get; set; }
            public string Scheme { get; set; }
            public string BearerFormat { get; set; }
            public object In { get; set; }
            public string Description { get; set; }
            public OpenApiReference Reference { get; internal set; }
        }

        internal class OpenApiInfo : OpenApi.OpenApiInfo
        {
            public string Title { get; set; }
            public string Version { get; set; }
        }

        internal class OpenApiSecurityRequirement
        {
        }

        internal class OpenApiReference
        {
            public object Type { get; set; }
            public string Id { get; set; }
        }
    }
}