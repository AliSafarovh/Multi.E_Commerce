using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace MultiShop.OcelotGateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAuthentication().AddJwtBearer("OcelotAuthenticationScheme", opt =>
            {
                opt.Authority = builder.Configuration["IdentityServerUrl"];
                opt.Audience = "ResourceOcelot";
                opt.RequireHttpsMetadata = false;
            });

            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("ocelot.json").Build();
            builder.Services.AddOcelot(configuration);

            var app = builder.Build();

            await app.UseOcelot();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
