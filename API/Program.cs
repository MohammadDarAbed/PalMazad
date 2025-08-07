using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Email;
using Business.Infrastructure;
using Business.Middlewares;
using Microsoft.EntityFrameworkCore;
using Observer;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add RabbitMQ + publishers + consumers
builder.Services.AddRabbitMq("rabbitmq://localhost", "admin", "1162016");

//builder.Services.AddDbContext<BaseDbContext>(options =>
//    options.UseSqlServer(connectionString));
// Register business layer.
// Replace built-in DI with Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Register Autofac modules
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new DependencyInjectionModule(builder.Configuration));
});
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(EmailEventHandler).Assembly);
});
//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
//        options.JsonSerializerOptions.MaxDepth = 64;
//    });

// Corse Policy:
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

//app.UseMiddleware<RateLimitingMiddleware>();
app.UseMiddleware<ProfilingMiddleware>();

app.MapControllers();

app.Run();
