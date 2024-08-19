using API.DbContexts;
using API.UserService.Repository;
using DataAccess.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<InterviewDBContext>(options =>
{
    options.UseSqlServer(
       builder.Configuration.GetConnectionString("InterviewDB"),
       sqlServerOptionsAction: sqlOptions =>
       {
           sqlOptions.EnableRetryOnFailure(
               maxRetryCount: 5,
               maxRetryDelay: TimeSpan.FromSeconds(10),
               errorNumbersToAdd: null);
       }
    );
}, ServiceLifetime.Scoped);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<DatabaseService>();

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
