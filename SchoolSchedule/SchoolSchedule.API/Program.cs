using CleanArchitectureSolution.Entities;
using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Interfaces;
using SchoolSchedule.Application.Services;
using SchoolSchedule.Core.Interfaces;
using SchoolSchedule.Infrastructure.Data;
using SchoolSchedule.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Dodanie usług do kontenera.
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<SchoolScheduleContext>(options =>
        options.UseInMemoryDatabase("SchoolScheduleDb"));
}
else
{
    builder.Services.AddDbContext<SchoolScheduleContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
}

builder.Services.AddScoped<IGenericRepository<Lesson>, GenericRepository<Lesson>>();
builder.Services.AddScoped<IGenericRepository<Grade>, GenericRepository<Grade>>();
builder.Services.AddScoped<IGenericRepository<Attendance>, GenericRepository<Attendance>>();

builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();

builder.Services.AddControllers();

// Dodanie Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "SchoolSchedule API", Version = "v1" });
});

var app = builder.Build();

// Konfiguracja potoku przetwarzania żądań.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // Użycie Swagger
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolSchedule API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();