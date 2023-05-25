using H4_Software_test.Areas.Identity;
using H4_Software_test.Data;
using H4_Software_test.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure your server to require certificates
// https://learn.microsoft.com/en-us/aspnet/core/security/authentication/certauth?view=aspnetcore-7.0#configure-your-server-to-require-certificates
builder.Services.Configure<KestrelServerOptions>(options =>
{
    // use the custom certificate
    options.ConfigureHttpsDefaults(o =>
    {
        o.ClientCertificateMode = ClientCertificateMode.RequireCertificate; // to force user authentication
        //instanciate the certificate
        X509Certificate2 cert = null;
        if (Environment.OSVersion.Platform == PlatformID.Unix)
        {
            cert = new X509Certificate2("/home/nwj/.aspnet/https/aspnetapp.pfx", "YourPassword");
        }
        else if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            cert = new X509Certificate2(@"C:\Skole\Afgangsprojekt\H4-Software-test\Certs\.aspnet\https\aspnetapp.pfx", "436407"); // put the actual certificate file location and password here

        }
        if (cert is not null)
        {
            o.ServerCertificate = cert;
            o.ClientCertificateValidation = (certificate2, chain, errors) =>
            {
                // TODO: Implement certificate validation logic if needed
                return true;
            };
        }
    });
});

// Changes password requirements

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;

    //cercificate settings
    options.SignIn.RequireConfirmedAccount = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;


});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

// Disse blev brugt da jeg ville oprette tekstfiler i databasen
//builder.Services.AddScoped<ITextFilesService, TextFilesService>();
//builder.Services.AddScoped<ITextFilesRepo, TextFilesRepo>();

builder.Services.AddAuthorization(options =>
{
    //add authentication for user
    options.AddPolicy("RequireUserRole",
                      policy => policy.RequireAuthenticatedUser());
    options.AddPolicy("RequireAdministratorRole",
               policy => policy.RequireRole("admin"));
});
// Read the connection string from the appsettings.json file

// Set the database connection for the custom DbContext class

builder.Services.AddDbContext<aspnetH4Softwaretest3a56d5607b184dc6bb4f36cb9fd57e40Context>(options =>

options.UseSqlServer(

    builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
