using System.Reflection;
using Web.Hubs;
using Web.Mapper;

var builder = WebApplication.CreateBuilder(args);

// SigalR: manual added signalR dependency
builder.Services.AddSignalR();

// Add services to the container.
builder.Services.AddControllersWithViews();

// manual add:
builder.Services.AddSession(); //  using session  
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
app.MapHub<DomainEventHub>("/DomainEventHub");

app.Run();
