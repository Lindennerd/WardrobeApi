using System.Text;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using Wardrobe.Application.Cloth;
using Wardrobe.Application.Image.BackgroundRemoval;
using Wardrobe.Application.Image.Upload;
using Wardrobe.Application.Security;
using Wardrobe.Application.User;
using Wardrobe.CrossCutting.Registrations;
using Wardrobe.Infra.Database;
using Wardrobe.Infra.Database.Cloth;
using Wardrobe.Infra.HttpClients;

var builder = WebApplication
    .CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Wardrobe API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.Configure<MongoConnectionSettings>(
    builder.Configuration.GetSection("MongoDB")
);
builder.Services.UseAzure(builder.Configuration);

builder.Services.AddScoped<IClothesRepository, ClothesRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IIdentityRepository, IdentityRepository>();
builder.Services.AddScoped<IBackgroundRemovalQueueService, BackgroundRemovalQueueService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IClothService, ClothService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUploadImageService, UploadImageService>();

builder.Services.UseGeolocationHttpClient(builder.Configuration);
builder.Services.UseWeatherHttpClient(builder.Configuration);

var tokenConfig = builder.Configuration.GetSection("TokenConfiguration");
builder.Services.AddScoped<ITokenService, TokenService>(opt => new TokenService(new TokenConfiguration
{
    Secret = tokenConfig["Secret"],
    ExpirationHours = int.Parse(tokenConfig["Expiration"])
}));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfig["Secret"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitMqConfiguration = builder.Configuration.GetSection("RabbitMQ");
        
        cfg.Host(rabbitMqConfiguration["Host"]);
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddCors( options =>
{
    options.AddPolicy("AllowAll", corsBuilder =>
    {
        corsBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddNLog(builder.Configuration.GetSection("Logging"));
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();