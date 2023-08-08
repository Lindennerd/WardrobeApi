using MassTransit;
using Wardrobe.Application.Image.BackgroundRemoval;
using Wardrobe.Application.Image.Database;
using Wardrobe.Domain.Repository;
using Wardrobe.Infra.Database;
using Wardrobe.Infra.Database.Cloth;

var builder = WebApplication
    .CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoConnectionSettings>(
    builder.Configuration.GetSection("MongoDB")
);

builder.Services.AddSingleton<IRepositoryBase<ClothesModel>, ClothesRepository>();
builder.Services.AddSingleton<ISaveImage, SaveImage>();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitMqConfiguration = builder.Configuration.GetSection("RabbitMQ");
        
        cfg.Host(rabbitMqConfiguration["Host"]);
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddScoped<IBackgroundRemovalQueueService, BackgroundRemovalQueueService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();