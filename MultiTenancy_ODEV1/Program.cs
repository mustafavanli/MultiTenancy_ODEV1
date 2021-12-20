using MultiTenancy_ODEV1;
using MultiTenancy_ODEV1.Attribute;
using System.Web.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllerRoute(name: "Default1",
           pattern: "{controller}/{TenantId}/{action}",
           defaults: new { controller = "{controller}", action = "{action}", id = UrlParameter.Optional });
app.MapControllerRoute(name: "Default2",
           pattern: "{controller}/{action}/{TenantId}",
           defaults: new { controller = "{controller}", action = "{action}", id = UrlParameter.Optional });
app.MapControllerRoute(name: "Default2",
           pattern: "{controller}/{action}",
           defaults: new { controller = "{controller}", action = "{action}"});
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();