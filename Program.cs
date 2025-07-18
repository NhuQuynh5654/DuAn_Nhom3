using DuAnThucTap.Data;
using DuAnThucTap.IRepository;
using DuAnThucTap.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<SchoolDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// "DefaultConnection" sẽ là chuỗi kết nối đến PostgreSQL của bạn

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Đăng ký Service (Service giờ đây trực tiếp kết nối DB)
builder.Services.AddScoped<ISchoolYearService, SchoolYearService>();
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
