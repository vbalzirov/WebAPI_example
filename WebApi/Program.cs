using CompanyName.Application.Dal.Orders.Configuration;
using CompanyName.Application.Dal.Orders.Contexts;
using CompanyName.Application.Dal.Orders.Repositories;
using CompanyName.Application.Services.ProductService.Services;
using CompanyName.Application.WebApi.OrdersApi.Configuratioin;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// создается каждый раз, когда его запрашивают, даже если это лдин и тот же объект.
// builder.Services.AddTransient

// создается всега один раз
// builder.Services.AddSingleton

CreateConfiguration(builder);

// создаются единожды для каждого запроса
builder.Services.AddSingleton<OrderContext>();
builder.Services.AddScoped<IOrdersDbRepository, OrdersDbRepository>();
builder.Services.AddScoped<IOrdersService, OrdersService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

void CreateConfiguration(WebApplicationBuilder builder)
{
    var section = builder.Configuration.GetSection(nameof(OrderRepositorySettings))
        .Get<OrderRepositorySettings>();

    builder.Services.AddSingleton<IOrderRepositorySettings>(section);
}
