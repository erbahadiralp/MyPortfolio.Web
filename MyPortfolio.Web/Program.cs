using Microsoft.AspNetCore.Identity;
using System.Threading.RateLimiting;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Web.Models;
using MyPortfolio.Web.Models.Entities;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

// Add services to the container.
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddDbContext<MyPortfolioDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    npgsqlOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorCodesToAdd: null);
    }));

builder.Services.AddIdentity<Admin, IdentityRole>(options =>
{
    // Geliştirme ortamı için şifre kurallarını yapılandır
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;

    // Brute Force Protection
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
})
    .AddEntityFrameworkStores<MyPortfolioDbContext>();

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy", policy =>
    {
        // Replace with your actual domain when deploying
        policy.WithOrigins("https://bahadiralper.com")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    // Global Rate Limit
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 100,
                QueueLimit = 0,
                Window = TimeSpan.FromMinutes(1)
            }));

    // Strict Rate Limit for Login: 5 attempts per minute
    options.AddFixedWindowLimiter("strict", opt =>
    {
        opt.Window = TimeSpan.FromMinutes(1);
        opt.PermitLimit = 5;
        opt.QueueLimit = 0;
    });
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
    });

builder.Services.AddControllersWithViews()
    .AddViewLocalization();

builder.Services.AddHttpClient();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("tr-TR")
    };

    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new QueryStringRequestCultureProvider(),
        new CookieRequestCultureProvider()
    };
});

// Configure Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Warning);

var app = builder.Build();

app.UseForwardedHeaders();

app.UseRequestLocalization();

// apply migrations and seed database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<MyPortfolioDbContext>();
        
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        var npgsqlBuilder = new NpgsqlConnectionStringBuilder(connectionString);
        var targetDbName = npgsqlBuilder.Database;
        npgsqlBuilder.Database = "postgres";
        var masterConnectionString = npgsqlBuilder.ConnectionString;
        
        using (var sqlConnection = new NpgsqlConnection(masterConnectionString))
        {
            await sqlConnection.OpenAsync();
            var checkCmd = new NpgsqlCommand($"SELECT 1 FROM pg_database WHERE datname = '{targetDbName}'", sqlConnection);
            var exists = await checkCmd.ExecuteScalarAsync() != null;
            if (!exists)
            {
                var sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = $"CREATE DATABASE \"{targetDbName}\"";
                await sqlCommand.ExecuteNonQueryAsync();
            }
        }

        await context.Database.MigrateAsync();
        
        await DbSeeder.SeedRolesAndAdminAsync(services, loggerFactory);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during migration or seeding.");
        throw;
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("DefaultPolicy");
app.UseRateLimiter();

// Security Headers Middleware
app.Use(async (context, next) =>
{
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["X-Frame-Options"] = "SAMEORIGIN";
    context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
    context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
    context.Response.Headers["Cross-Origin-Resource-Policy"] = "same-origin";
    // fix: Cross-Origin-Embedder-Policy header missing [90004]
    context.Response.Headers["Cross-Origin-Embedder-Policy"] = "unsafe-none";
    context.Response.Headers["Permissions-Policy"] = "camera=(), microphone=(), geolocation=(), payment=()";
    context.Response.Headers["Content-Security-Policy"] =
        "default-src 'self'; " +
        "script-src 'self' 'unsafe-inline' 'unsafe-eval' https://cdn.jsdelivr.net https://fonts.googleapis.com https://challenges.cloudflare.com blob:; " +
        "style-src 'self' 'unsafe-inline' https://cdn.jsdelivr.net https://fonts.googleapis.com https://fonts.gstatic.com; " +
        "font-src 'self' https://fonts.gstatic.com https://fonts.googleapis.com; " +
        "img-src 'self' data:; " +
        "frame-src 'self' https://challenges.cloudflare.com; " +
        "connect-src 'self' https://challenges.cloudflare.com; " +
        "object-src 'none'; " +
        "worker-src 'self' blob: https://cdn.jsdelivr.net; " +
        // fix: CSP no-fallback directives [10055]
        "form-action 'self'; " +
        "frame-ancestors 'self'; " +
        "base-uri 'self';";
    await next();
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
