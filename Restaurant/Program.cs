using Microsoft.OpenApi.Models;
using Restaurant.API;
using Restaurant.API.Data.Contexts;
using Restaurant.API.Repositories.Admins;
using Restaurant.API.Repositories.Categories;
using Restaurant.API.Repositories.Foods;
using Restaurant.API.Repositories.Orders;
using Restaurant.API.Services.Admins;
using Restaurant.API.Services.Categories;
using Restaurant.API.Services.Foods;
using Restaurant.API.Services.Orders;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // configure swagger to accept file uploads
    c.OperationFilter<SwaggerFileUploadFilter>();

    // set the comments path for the swagger json and ui
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);

    var xmlPath = Path.Combine(Environment.CurrentDirectory, "bin", "Debug", "net6.0", "Restaurant.API.xml");
    c.IncludeXmlComments(xmlPath);

});

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


var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowLocalhost3000");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "images",
        pattern: "Images/{savedFilename}/{fileName}",
        defaults: new { controller = "YourController", action = "GetUploadedImage" });

    // Add any other endpoint mappings here
    endpoints.MapControllers();
});


app.UseAuthorization();

app.MapControllers();

app.Run();
