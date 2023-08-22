using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Web.CommonApi;
using Web.Filters;

namespace Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "ApiDefault", id = RouteParameter.Optional }
            );

            // Remove the XML formatter
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Compression
            config.MessageHandlers.Insert(0, new CompressionHandler());
            var httpClient = HttpClientFactory.Create(new DecompressionHandler());
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
        }
    }
}
