using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("UserConnection"))
);

builder.Services.AddIdentity<IdentityUser<int>, IdentityRole<int>>()
                .AddEntityFrameworkStores<UserDbContext>();

builder.Services.AddScoped<RegisterService, RegisterService>();
builder.Services.AddScoped<LoginService, LoginService>();
builder.Services.AddScoped<TokenService, TokenService>();
builder.Services.AddScoped<LogoutService, LogoutService>();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen();

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
