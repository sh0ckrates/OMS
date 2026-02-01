using Application.Services.Concrete;
using Application.Services.Concrete.OMS.Domain.Policies;
using Application.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

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
