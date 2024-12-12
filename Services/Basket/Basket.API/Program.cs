using Asp.Versioning;
using Basket.Application.Commands;
using Basket.Application.GrpcService;
using Basket.Core.Repositories;
using Basket.Infrastructure.Repositories;
using Common.Logging;
using Discount.Grpc.Protos;
using MassTransit;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Add serilog configuration
builder.Host.UseSerilog(Logging.ConfigureLogger);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApiVersioning(opt =>
{
    opt.ReportApiVersions = true;
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
})
//add Version APi explorer to support versioning 
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
    new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Basket.API", Version = "v1" });
    c.SwaggerDoc("v2",
    new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Basket.API", Version = "v2" });

    //configure swagger to use versioning 
    c.DocInclusionPredicate((version, apiDescription) =>
    {
        if (!apiDescription.TryGetMethodInfo(out var method))
        {
            return false;
        }
        var versions = method.DeclaringType?
        .GetCustomAttributes(true)
        .OfType<ApiVersionAttribute>()
        .SelectMany(attr => attr.Versions);

        return versions?.Any(v => $"v{v.ToString()}" == version) ?? false;
    });

});
builder.Services.AddAutoMapper(typeof(Program).Assembly);
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(CreateShoppingCartCommand).Assembly
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
builder.Services.AddScoped<IBasketRepository, BasketRepository>();


//Redis 
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});
builder.Services.AddLogging();
//GRPC
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opt =>
{
    opt.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
});

//Masstransit
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ct, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
});

builder.Services.AddMassTransitHostedService();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API v1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "Basket.API v2");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
