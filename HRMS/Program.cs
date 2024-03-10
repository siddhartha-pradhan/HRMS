using System.Net;
using Microsoft.OpenApi.Models;
using HRMS.Infrastructure.Dependency;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = builder.Configuration;

services.AddInfrastructureService(configuration);

services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

// services.AddIdentityService(configuration);

services.AddHttpContextAccessor();

services.AddControllersWithViews();

services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

services.AddRazorPages();

services.ConfigureApplicationCookie(options =>
{
    options.LogoutPath = $"/User/Account/Logout";
    options.LoginPath = $"/User/Account/Login";
    options.AccessDeniedPath = $"/User/Account/AccessDenied";
});

services.AddEndpointsApiExplorer();

services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "HRMS API",
        Description = "HRMS .NET Core API 6.0"
    });
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseExceptionHandler("/Home/Error");

app.UseHsts();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.UseStatusCodePages(context =>
{
    var response = context.HttpContext.Response;

    if (response.StatusCode.Equals((int)HttpStatusCode.NotFound))
    {
        response.Redirect("/Home/Error404");
    }
    if (response.StatusCode.Equals((int)HttpStatusCode.Unauthorized))
    {
        response.Redirect("/Home/Error401");
    }
    if (response.StatusCode.Equals((int)HttpStatusCode.Forbidden))
    {
        response.Redirect("/Home/Error403");
    }

    return Task.CompletedTask;
});

app.Use(async (context, next) =>
{
    var token = context.Session.GetString("JWTToken");

    if (!string.IsNullOrEmpty(token))
    {
        context.Request.Headers.Append("Authorization", "Bearer " + token);
    }

    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");

// using (var scope = app.Services.CreateScope())
// {
//     var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
//
//     dbInitializer.Initialize();
// }

app.Run();