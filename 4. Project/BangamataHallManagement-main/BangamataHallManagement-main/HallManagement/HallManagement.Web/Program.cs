using HallManagement.Model.Entities;
using HallManagement.Model.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Web.Classes;
using Web.Extensions;
using NLog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureRepositoryWrapper();
builder.Services.ConfigureService();
builder.Services.AddControllers();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(option => option.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Host.ConfigureLogging((hostingContext, logging) =>
{
    logging.ClearProviders();
    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    logging.AddNLog();
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(600);
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc();
builder.Services.Configure<_AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddDbContext<BangamataHallContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string have some issues.")));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
