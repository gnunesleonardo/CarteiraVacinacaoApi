using CarteiraVacinacaoApi.Api.Middlewares;
using CarteiraVacinacaoApi.Application;
using CarteiraVacinacaoApi.Application.Behaviors;
using CarteiraVacinacaoApi.Infrastructure;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddMemoryCache();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

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
