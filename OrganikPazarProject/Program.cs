using Microsoft.EntityFrameworkCore;
using OrganikPazar.Context;
using OrganikPazar.DAL.Interfaces;
using OrganikPazar.DAL.Repositories;
using OrganikPazar.Hubs;
using OrganikPazar.Service.Interfaces;
using OrganikPazar.Service.Managers;
using OrganikPazar.Services.Interfaces;
using OrganikPazar.Services.Managers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<OrganikPazarContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();

builder.Services.AddHttpClient<IAIService, AIService>();

builder.Services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));

builder.Services.AddScoped<IProductSuggestionService, ProductSuggestionService>();
builder.Services.AddScoped<INaturalQueryService, NaturalQueryService>();
builder.Services.AddScoped<IForecastService, ForecastService>();
var app = builder.Build();

app.MapHub<ChatHub>("/chathub");
app.MapHub<AIRecipeHub>("/aIRecipeHub");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseStaticFiles();


app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
