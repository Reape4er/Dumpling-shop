using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Users.API.Mappings;
using Users.API.Middlewares;
using Users.API.Services;
using Users.API.Utils;
using Users.db;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();

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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserService,UserService>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddTransient<IJWTUtils, JWTutils>();

var app = builder.Build();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());//дописать
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<JWTMiddleware>();

app.MapControllers();

app.Run();
