using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;

namespace MultiShop.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //jwt token
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                opt.LoginPath = "/Login/Index/";  //giris
                opt.LogoutPath = "/Login/LogOut/"; //cixis
                opt.AccessDeniedPath = "/Pages/AccessDanied/"; //yetkisiz
                opt.Cookie.HttpOnly = true; //https olmadan
                opt.Cookie.SameSite = SameSiteMode.Strict;
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                opt.Cookie.Name = "MultiShopJwt"; //cookie ismi 
            });
            // Cookie Authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie
                (CookieAuthenticationDefaults.AuthenticationScheme, opt =>
                {
                    opt.LoginPath = "/Login/Index/";
                    opt.ExpireTimeSpan = TimeSpan.FromDays(5);
                    opt.Cookie.Name = "MultiShopCookie";
                    opt.SlidingExpiration = true;
                });


            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddHttpClient<IIdentityService, IdentityService>();

            builder.Services.AddHttpClient();
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
            builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));

            builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();
            //builder.Services.AddScoped<ClientCredentialTokenHandler>();
            var values = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
            builder.Services.AddHttpClient<IUserService, UserService>(opt =>
            {
                opt.BaseAddress = new Uri(values.IdentityServerUrl);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Default}/{action=Index}/{id?}");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.Run();
        }
    }
}
