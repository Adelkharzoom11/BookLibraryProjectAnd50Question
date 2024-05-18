using BookLibrary.Data.Interfaces;
using BookLibrary.Data.Services;
using BookLibrary.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });


builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend", builder =>
        {
            builder.WithOrigins("file:///E:/UI_For_Boobs/index.html")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
    });

// reagster the services
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ICategoryServicecs, CategoryService>();

// Configure and register BookService with storage path from configuration
builder.Services.AddScoped<IBookService, BookService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var storagePath = configuration.GetSection("StorageSettings:StoragePath").Value;
    var context = provider.GetRequiredService<ApplicationDbContext>();
    return new BookService(context, storagePath);
});

// Db Context
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
});





// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
