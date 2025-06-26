using Netby.Todo.Site.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpClient("TodoApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7077/api/v1/"); // Todo: Obtener desde el appsettings
});


// para hacer login
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services
    .AddSiteApiModule(builder.Configuration);

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

app.MapFallbackToPage("/Login");

app.MapRazorPages();

app.UseSession();

app.Run();
