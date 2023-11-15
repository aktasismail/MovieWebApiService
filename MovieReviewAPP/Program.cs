using BusinessLayer.Abstact;
using DataAccessLayer.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieReviewAPP.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
}).
    AddApplicationPart(typeof(PresentationLayer.AssemblyRefence).Assembly)
    .AddXmlDataContractSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.DbContextConfigure(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLogService();
builder.Services.ConfigureLinks();
builder.Services.ConfigureFilter();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureCors();
builder.Services.ConfigureCustomMediaType();
builder.Services.ConfigureDataShaper();
builder.Services.ConfigureVerisoning();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogService>();
app.HandleException(logger);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsProduction())
{
    app.UseHsts();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
