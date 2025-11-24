/*
 * FILE : Program.cs
 * PROJECT : SENG3020 - Flight Data Management System
 * PROGRAMMER : Nicholas Aguilar
 * FIRST VERSION : 2025-11-22
 * DESCRIPTION : Main program file for the Ground terminal station application.
 */
using FDMS_GroundStation_API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options => {
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<GtsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Initialize the database
DbInitializer.Initialize(app.Services);

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();