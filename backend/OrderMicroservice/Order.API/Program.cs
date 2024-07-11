using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Order.API.Mappings;
using Order.API.Services;
using Order.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IOrderService, OrderService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<MainContext>(options =>
    options
        .UseNpgsql(builder.Configuration.GetConnectionString("UsersPostgres"))
        .UseSnakeCaseNamingConvention()
        );

builder.Services.AddSingleton(provider => new MapperConfiguration(config =>
{
    config.AddProfile(provider.GetService<MappingProfile>());
}).CreateMapper());

builder.Services.AddSingleton<MappingProfile>();

var app = builder.Build();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
