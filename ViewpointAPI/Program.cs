using dotenv.net;
using ViewpointAPI.Models;
using ViewpointAPI.Services;
using ViewpointAPI.Repositories;
using ViewpointAPI.Exceptions;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
string connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRING");
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
builder.Configuration["SecurityDatabase:ConnectionString"] = connectionString;

builder.Services.Configure<SecurityDatabaseSettings>(builder.Configuration.GetSection("SecurityDatabase"));

builder.Services.AddSingleton<IHistoryService, HistoryService>();
builder.Services.AddSingleton<IReferenceService, ReferenceService>();
builder.Services.AddSingleton<IIdsService, IdsService>();

builder.Services.AddSingleton<IHistoryRepository, HistoryRepository>();
builder.Services.AddSingleton<IReferenceRepository, ReferenceRepository>();
builder.Services.AddSingleton<IIdsRepository, IdsRepository>();

builder.Services.AddMemoryCache();
builder.Services.AddHostedService<CacheHydrationService>();

builder.Services.AddSingleton<CustomException>();

builder.Services.AddControllers().AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ViewpointAPI",
        Version = "v1"
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
