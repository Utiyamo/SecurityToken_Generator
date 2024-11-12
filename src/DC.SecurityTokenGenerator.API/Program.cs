using DC.SecurityTokenGenerator.Domain.Command;
using DC.SecurityTokenGenerator.Domain.Entities;
using DC.SecurityTokenGenerator.Domain.Handler;
using DC.SecurityTokenGenerator.Domain.Models;
using DC.SecurityTokenGenerator.Domain.Repositories;
using DC.SecurityTokenGenerator.Infrastructure.Repositories;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.AddTransient<IRequestHandler<CreateTokenCommand, BaseResponse<TokenResult>>, CreateTokenHandler>();

builder.Services.AddTransient<IAESRepository, AESRepository>();
builder.Services.AddTransient<IRC4Repository, RC4Repository>();
builder.Services.AddTransient<I3DESRepository, _3DESRepository>();
builder.Services.AddTransient<IIDEARepository, IDEARepository>();

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
