using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Vertical_Slice_Architecture.Database;
using Vertical_Slice_Architecture.Filters;
using Vertical_Slice_Architecture.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(
    opt =>
    {
        opt.Filters.Add(new ValidationFilter()); //Validate Filter Activated
    }
);
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = false;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));


builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        options.UseInMemoryDatabase("VerticalSliceDb");
    });

var app = builder.Build();

app.UseCustomException();

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
