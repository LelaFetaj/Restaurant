using Restaurant.API.Data.Contexts;
using Restaurant.API.Repositories.Admins;
using Restaurant.API.Repositories.Categories;
using Restaurant.API.Repositories.Foods;
using Restaurant.API.Repositories.Orders;
using Restaurant.API.Services.Admins;
using Restaurant.API.Services.Categories;
using Restaurant.API.Services.Foods;
using Restaurant.API.Services.Orders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RestaurantDbContext>();
builder.Services.AddScoped<RestaurantDbContext>();
builder.Services.AddTransient<IAdminRepository, AdminRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IFoodRepository, FoodRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IFoodService, FoodService>();
builder.Services.AddTransient<IOrderService, OrderService>();

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
