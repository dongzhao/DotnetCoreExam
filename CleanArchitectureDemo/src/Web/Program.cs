using Core.DomainEvents;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using System.Reflection;
using Web.Hubs;
using Web.Mapper;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(path: "appsettings.Development.json", optional: true, reloadOnChange: true)
    .Build();

//builder.Services.AddDbContext<AppDbContext>(d => d.UseSqlServer(config.GetConnectionString(nameof(AppDbContext))));
builder.Services.AddDbContext<AppDbContext>(d => d.UseInMemoryDatabase(nameof(AppDbContext)));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));

builder.Services.AddLogging(builder =>
{
    builder.AddConfiguration(config.GetSection("Logging"));
    builder.AddDebug();
    builder.AddConsole();
});

// add MediatR dependency injection
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetAssembly(typeof(OrderConfirmedHandler))));
builder.Services.AddScoped<IDomainEventDispatcher, MediatorDomainEventDispatcher>();
//builder.Services.AddScoped<INotificationHandler<OrderConfirmed>, DomainEventListener>();

// SigalR: manual added signalR dependency
builder.Services.AddSignalR();

// Add services to the container.
builder.Services.AddControllersWithViews();

// manual add:
builder.Services.AddSession(); //  using session  

// auto mapper
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));



// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// manual add: using session  
app.UseSession();

//app.UseMvc();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// SignalR: add chathub
//app.UseSignalR(cfg => { 

//});
app.MapHub<ChatHub>("/chathub");
//app.MapHub<DomainEventHub>("/domainEventHub");
app.MapHub<OrderConfirmedHub>("/orderConfirmedHub");

app.Run();
