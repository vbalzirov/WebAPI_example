using CompanyName.Application.Dal.Orders.Contexts;
using CompanyName.Application.Dal.Orders.Repositories;
using CompanyName.Application.Services.ProductService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CompanyName.Application.Dal.Auth.Configurations;
using CompanyName.Application.Dal.Auth.Repository;
using CompanyName.Application.Services.AuthService.Services;
using CompanyName.Application.Dal.Orders.Configuratioin;
using CompanyName.Application.WebApi.OrdersApi.Configuratioin;

var builder = WebApplication.CreateBuilder(args);

// создается каждый раз, когда его запрашивают, даже если это лдин и тот же объект.
// builder.Services.AddTransient

// создается всега один раз
// builder.Services.AddSingleton

CreateConfiguration(builder);

// создаются единожды для каждого запроса
builder.Services.AddSingleton<OrderContext>();
builder.Services.AddSingleton<AuthContext>();

builder.Services.AddScoped<IOrdersDbRepository, OrdersDbRepository>();
builder.Services.AddScoped<IOrdersService, OrdersService>();

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

CreateAuthenticationDependencies(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

void CreateConfiguration(WebApplicationBuilder builder)
{
    var orderRepositorySection = builder.Configuration.GetSection("OrderRepositorySettings")
        .Get<OrderRepositorySettings>();

    var authRepositorySection = builder.Configuration.GetSection("AuthRepositorySettings")
        .Get<AuthRepositorySettings>();

    builder.Services.AddSingleton<IOrderRepositorySettings>(orderRepositorySection);
    builder.Services.AddSingleton<IAuthRepositorySettings>(authRepositorySection);
}

void CreateAuthenticationDependencies(WebApplicationBuilder builder)
{
    // Get configuration for token generation
    var jwtConfig = builder.Configuration.GetSection("JwtSettings")
        .Get<JwtConfigurationSettings>();

    builder.Services.AddSingleton<IJwtConfigurationSettings>(jwtConfig);

    // Add auth dependencies
    builder.Services
    .AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(jwt => {
        var key = Encoding.ASCII.GetBytes(
            jwtConfig.Key);

        jwt.SaveToken = true;
        jwt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),

            // Just to avoid issues on localhost, it must be true on prod
            ValidateIssuer = false,
            ValidateAudience = false,

            // To avoid re-generation scenario just for develop
            RequireExpirationTime = false,

            ValidateLifetime = true
        };
    });

    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
        .AddEntityFrameworkStores<AuthContext>();
}
