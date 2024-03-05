using dotenv.net;
using ViewpointAPI.Models;
using ViewpointAPI.Services;
using ViewpointAPI.Repositories;
using ViewpointAPI.Exceptions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Jwt configuration starts here
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });
//Jwt configuration ends here

DotEnv.Load();

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
string connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRING");
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
builder.Configuration["SecurityDatabase:ConnectionString"] = connectionString;

builder.Services.Configure<SecurityDatabaseSettings>(builder.Configuration.GetSection("SecurityDatabase"));

builder.Services.AddSingleton<IHistoryService, HistoryService>();
builder.Services.AddSingleton<IReferenceService, ReferenceService>();
builder.Services.AddSingleton<IIdsService, IdsService>();
builder.Services.AddSingleton<IdsService>(); // Used for preload cache method in CacheHydrationService

builder.Services.AddSingleton<IHistoryRepository, HistoryRepository>();
builder.Services.AddSingleton<IReferenceRepository, ReferenceRepository>();
builder.Services.AddSingleton<IIdsRepository, IdsRepository>();

builder.Services.AddMemoryCache();

builder.Services.AddHostedService<CacheHydrationService>();

builder.Services.AddSingleton<IdNotFoundException>();

builder.Services.AddControllers().AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});

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
//if (app.Environment.IsDevelopment())
//{
//app.UseSwagger();
//app.UseSwaggerUI();
//}

// To enable Swagger in initial deployed version
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
