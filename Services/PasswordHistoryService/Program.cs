using Microsoft.EntityFrameworkCore;
using PasswordHistoryService.Data;
using PasswordHistoryService.Features.Application.Commands;
using PasswordHistoryService.Features.Application.Commands.Implementations;
using PasswordHistoryService.Features.Application.Dtos;
using PasswordHistoryService.Features.Application.Queries;
using PasswordHistoryService.Features.Application.Repositories;
using PasswordHistoryService.Features.Applications.Queries.Implementation;
using PasswordHistoryService.Features.Domain.Saga;
using PasswordHistoryService.Features.Infrastructure.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICreateCommand<CreateRequestDto>, CreateCommand>();
builder.Services.AddScoped<IGetAllQuery<QueryResultDto>, GetAllQuery>();
builder.Services.AddScoped<iRepository, Repository>();
builder.Services.AddTransient<Consumer>();

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddHostedService<Consumer>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database features
builder.Services.AddDbContext<PasswordHistoryContext>(
    options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("PasswordHistoryDbConnection")));

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