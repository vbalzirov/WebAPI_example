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
using NLog;
using NLog.Config;
using CompanyName.Application.Core.Jobs;

var builder = WebApplication.CreateBuilder(args);

// создается каждый раз, когда его запрашивают, даже если это лдин и тот же объект.
// builder.Services.AddTransient

// создается всега один раз
// builder.Services.AddSingleton

InjectSettingsConfiguration(builder);

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

InjectAuthenticationDependencies(builder);

InjectLogger(builder);

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

StartJobs();

void InjectSettingsConfiguration(WebApplicationBuilder builder)
{
    var orderRepositorySection = builder.Configuration.GetSection("OrderRepositorySettings")
        .Get<OrderRepositorySettings>();

    var authRepositorySection = builder.Configuration.GetSection("AuthRepositorySettings")
        .Get<AuthRepositorySettings>();

    builder.Services.AddSingleton<IOrderRepositorySettings>(orderRepositorySection);
    builder.Services.AddSingleton<IAuthRepositorySettings>(authRepositorySection);
}

void InjectAuthenticationDependencies(WebApplicationBuilder builder)
{
    // Get configuration for token generation
    var jwtConfig = builder.Configuration.GetSection("JwtSettings")
        .Get<JwtConfigurationSettings>();

    builder.Services.AddSingleton<IJwtConfigurationSettings>(jwtConfig);

    // Add auth dependencies
    builder.Services
    .AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("User", policy =>
                          policy.RequireClaim("User"));

        options.AddPolicy("Admin", policy =>
                      policy.RequireClaim("Admin"));

        options.AddPolicy("Sitter", policy =>
                      policy.RequireClaim("Sitter"));
    });

    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
        .AddEntityFrameworkStores<AuthContext>();
}

void InjectLogger(WebApplicationBuilder builder)
{
    var config = new LoggingConfiguration();

    // Targets where to log to: File and Console
    var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "Logs.txt" };
    var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

    // Rules for mapping loggers to targets            
    var minLoggingLevelRule = new LoggingRule();
    minLoggingLevelRule.SetLoggingLevels(NLog.LogLevel.Warn, NLog.LogLevel.Fatal);

    config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logconsole);
    config.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, logfile);
    config.AddRule(minLoggingLevelRule);

    // Apply config           
    LogManager.Configuration = config;

    var logger = LogManager.Setup().GetCurrentClassLogger();
    builder.Services.AddSingleton<NLog.ILogger>(logger);
}

void StartJobs()
{
    StatusCheckJob job = new StatusCheckJob();

    Thread statusCheckThread = new Thread(new ThreadStart(job.Start));
}
