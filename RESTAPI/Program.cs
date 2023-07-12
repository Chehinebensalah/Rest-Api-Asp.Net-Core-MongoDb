using RESTAPI.DataBase;
using RESTAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//SERVICES 
builder.Services.AddTransient<ICategoryService,CategoryService>();
builder.Services.AddTransient<IProductService, ProductService>();


// database Config 
builder.Services.Configure<DataBaseSettings>(
    builder.Configuration.GetSection("MyDb")   
);

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
