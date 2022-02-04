using CompanyEmployees.ActionFilters;
using CompanyEmployees.Extensions;
using CompanyEmployees.Utility;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using NLog;
using Repository.Extensions;


var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson()
    .AddCustomCsvFormatter()
    .AddXmlDataContractSerializerFormatters();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddCustomMediaTypes();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureVersioning();
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddScoped<ValidateMediaTypeAttribute>();

builder.Services.AddScoped<EmployeeLinks>();
builder.Services.AddScoped<IDataShaper<EmployeeDto>, DataShaper<EmployeeDto>>();
;
builder.Services.AddScoped<ValidateCompanyExistsAttribute>();
builder.Services.AddScoped<ValidateEmployeeForCompanyExistsAttribute>();
var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var versionDescription in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{versionDescription.GroupName}/swagger.json",versionDescription.GroupName.ToUpperInvariant());
        }
    });
}
else
{
    app.UseHsts();
}

app.ConfigureExceptionHandler();
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseAuthorization();

app.MapControllers();

app.Run();
