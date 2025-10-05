<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
using backend.Enity;

=======

using backend.Mappings;
using AutoMapper;
using backend;
>>>>>>> 98ae579 (upload source code)
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
var app = builder.Build();
<<<<<<< HEAD

=======
// Configure the HTTP request pipeline.
>>>>>>> 98ae579 (upload source code)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
<<<<<<< HEAD
=======
AutoMapperTest.RunTest();
>>>>>>> 98ae579 (upload source code)
app.Run();
