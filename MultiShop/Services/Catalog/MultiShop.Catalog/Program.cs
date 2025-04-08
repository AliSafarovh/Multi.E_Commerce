using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MultiShop.Catalog.Services.AboutServices;
using MultiShop.Catalog.Services.BrandServices;
using MultiShop.Catalog.Services.CategoryServices;
using MultiShop.Catalog.Services.ContactServices;
using MultiShop.Catalog.Services.FeatureServices;
using MultiShop.Catalog.Services.FeatureSliderServices;
using MultiShop.Catalog.Services.OfferDiscountServices;
using MultiShop.Catalog.Services.ProductDetailDetailServices;
using MultiShop.Catalog.Services.ProductDetailServices;
using MultiShop.Catalog.Services.ProductImageServices;
using MultiShop.Catalog.Services.ProductServices;
using MultiShop.Catalog.Services.SpecialOfferServices;
using MultiShop.Catalog.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// JWT Authentication konfiqurasiyasý
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        try
        {
            // JWT Authentication konfiqurasiyasý
            opt.Authority = builder.Configuration["IdentityServerUrl"];
            opt.Audience = "ResourceCatalog";
            opt.RequireHttpsMetadata = false;

            // X?talarýn izl?niþi üçün ?lav? etdiyimiz blok
            opt.Events = new JwtBearerEvents
            {
                OnChallenge = async context =>
                {
                    try
                    {
                        // ?g?r ErrorDescription boþdursa, baþqa m?lumatlarý da yoxla
                        if (string.IsNullOrEmpty(context.ErrorDescription))
                        {
                            Console.WriteLine("OnChallenge: Unknown error occurred.");
                        }
                        else
                        {
                            Console.WriteLine("OnChallenge: " + context.ErrorDescription);
                        }

                        // Burada daha çox m?lumat da ?lav? ed? bil?rs?n
                        Console.WriteLine("OnChallenge: Error: " + context.Error);

                        // Geri qaytarma prosesini düzgün þ?kild? idar? edirik
                        await Task.CompletedTask;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("OnChallenge error: " + ex.Message);
                        Console.WriteLine("StackTrace: " + ex.StackTrace);
                    }
                },
                OnAuthenticationFailed = async context =>
                {
                    try
                    {
                        // X?tanýn detallarýný daha çox görm?k üçün
                        Console.WriteLine("Auth failed: " + context.Exception.Message);
                        Console.WriteLine("StackTrace: " + context.Exception.StackTrace);

                        await Task.CompletedTask;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("OnAuthenticationFailed error: " + ex.Message);
                        Console.WriteLine("StackTrace: " + ex.StackTrace);
                    }
                },
                OnMessageReceived = async context =>
                {
                    try
                    {
                        // Token alýndýðýnda ?lav? m?lumat
                        Console.WriteLine("Token alýndý: " + context.Token);

                        await Task.CompletedTask;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("OnMessageReceived error: " + ex.Message);
                        Console.WriteLine("StackTrace: " + ex.StackTrace);
                    }
                }
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine("JWT Bearer configuration error: " + ex.Message);
            Console.WriteLine("StackTrace: " + ex.StackTrace);
        }
    });

// Servisl?rin DI (Dependency Injection) qeydiyyatý
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductDetailService, ProductDetailService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IFeatureSliderService, FeatureSliderService>();
builder.Services.AddScoped<ISpecialOfferService, SpecialOfferService>();
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<IOfferDiscountService, OfferDiscountService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<IContactService, ContactService>();

// AutoMapper konfiqurasiyasý
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// DatabaseSettings konfiqurasiyasý
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSetting"));
builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

// Controller-l?rin ?lav? olunmasý
builder.Services.AddControllers();

// Swagger konfiqurasiyasý
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Please enter JWT Bearer token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

// Development mühitind? Swagger-i aktivl?þdir
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware-l?ri ?lav? et
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
