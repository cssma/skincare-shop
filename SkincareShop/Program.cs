using KoreanSkincareShop.Models.Interfaces;
using KoreanSkincareShop.Models.Services;
using KoreanSkincareShop.Models.Services.Listeners;
using KoreanSkincareShop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Core services
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

// Payment event listeners - scoped to match PaymentService lifetime
// Resolved automatically when IPaymentService is resolved via PaymentListenerInitializer
builder.Services.AddScoped<PaymentEmailNotificationService>();
builder.Services.AddScoped<PaymentAnalyticsService>();
builder.Services.AddScoped<PaymentInventoryUpdateService>();
builder.Services.AddScoped<PaymentListenerInitializer>();

builder.Services.AddDbContext<SkincareShopDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SkincareShopDbContextConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<SkincareShopDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/Login";
});

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Middleware to initialize payment listeners for each request scope
app.Use(async (context, next) =>
{
    // Resolve the initializer which triggers listener subscription
    context.RequestServices.GetService<PaymentListenerInitializer>();
    await next();
});

app.MapStaticAssets();

app.MapRazorPages();
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
