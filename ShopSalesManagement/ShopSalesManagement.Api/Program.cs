using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Api.Services;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShopSalesManagement.Domain;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// ���������� ������� ��������� ���� ������
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});


// ���������� �������� � ������������
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ������������� ������� ��� �������� ������
builder.Services.AddSingleton<List<ProductDto>>();
builder.Services.AddSingleton<List<StockDto>>();
builder.Services.AddSingleton<List<StoreDto>>();
builder.Services.AddSingleton<List<SaleDto>>();

// ����������� ��������� �������� ��� ������ ��������
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<StockService>();
builder.Services.AddScoped<StoreService>();
builder.Services.AddScoped<SaleService>();
builder.Services.AddScoped<ProductGroupService>();
builder.Services.AddScoped<PurchaseService>();

var app = builder.Build();

// ������������ HTTP-��������
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
