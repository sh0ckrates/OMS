using Application.Services.Concrete;
using Application.Services.Interfaces;
using Application.Services.Interfaces.Repo;
using Microsoft.EntityFrameworkCore;
using OMS.Infrastructure.Data;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IDiscountCategoryRepository, DiscountCategoryRepository>();

builder.Services.AddScoped<IDiscountPolicy, PriceListDiscountPolicy>();
builder.Services.AddScoped<IDiscountPolicy, PromotionDiscountPolicy>();
builder.Services.AddScoped<IDiscountPolicy, CouponDiscountPolicy>();
builder.Services.AddScoped<IDiscountEngine, DiscountEngine>();
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
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
