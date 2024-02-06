using dotenv.net;
using ViewpointAPI.Models;
using ViewpointAPI.Services;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();

// Add services to the container.
string connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRING");
builder.Configuration["SecurityDatabase:ConnectionString"] = connectionString;

builder.Services.Configure<SecurityDatabaseSettings>(builder.Configuration.GetSection("SecurityDatabase"));

builder.Services.AddSingleton<DataService>();

builder.Services.AddControllers().AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
