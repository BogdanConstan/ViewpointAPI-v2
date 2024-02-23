using dotenv.net;
using ViewpointAPI.Models;
using ViewpointAPI.Services;
using ViewpointAPI.Repositories;
using ViewpointAPI.Exceptions;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();

string connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRING");
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
