using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Product.API.Services;
using Product.DB;
using Users.API.Mappings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
// Add services to the container.

builder.Services.AddControllers();

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

builder.Services.AddTransient<IProductService, ProductService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); 

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
