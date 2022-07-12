using FInDashboardWASM.Server;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

string conn =   builder.Configuration.GetConnectionString("ProdDB");

builder.Services.AddDbContext<MssqDBContext>(options => options.UseSqlServer(conn));

builder.Services.AddSwaggerGen();

var apiKey = builder.Configuration["api_key"];




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor API V1");
    });

}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();



app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
