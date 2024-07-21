var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", false, true)
    .Build();


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient("API", client =>
{


    client.BaseAddress = new Uri("https://localhost:7075"); 
});
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();



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
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapGet("/", c =>
    {
        c.Response.Redirect("/Homes");
        return Task.CompletedTask;
    });
});

app.Run();
