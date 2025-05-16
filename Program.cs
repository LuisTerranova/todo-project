using Microsoft.AspNetCore.Authentication.Cookies;
using Todo.Data;


var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.Run();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllersWithViews()
        .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
    builder.Services.AddDbContext<TodoDataContext>();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            //options.LogoutPath = "/Account/Logout";
            //options.AccessDeniedPath = "/Account/AccessDenied";
            
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Lax;
        });
    builder.Services.AddAuthorization();
}
