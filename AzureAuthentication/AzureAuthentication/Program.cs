using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

using Microsoft.AspNetCore.Authentication;
using AzureAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));


// Add services to the container.

builder.Services.AddAuthentication(defaultScheme: AzureADDefaults.AuthenticationScheme)
       .AddAzureAD(options =>
       {
           options.Instance = "https://login.microsoftonline.com/";
           options.ClientId = "a4c48329-b8a4-49a3-9dbf-b809e2b28a8f";
           options.TenantId = "13d09fb8-63f3-45e8-891b-380ed222b9ae";
       });
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
