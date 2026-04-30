using Chores.Core.Interfaces;
using Chores.Core.Services;
using Chores.Infrastructure.Repositories;
using Chores.Infrastructure.Services;
using Hangfire;
using HousemateChoreReminderAPI.Chores.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repositories
builder.Services.AddScoped<IHousemateRepository, HousemateRepository>();
builder.Services.AddScoped<IChoreRepository, ChoreRepository>();
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<IReminderRepository, ReminderRepository>();

//Services
builder.Services.AddScoped<IChoreService, ChoreService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IHousemateService, HousemateService>();
builder.Services.AddScoped<IAssignmentService, AssignmentService>();
builder.Services.AddScoped<IReminderService, ReminderService>();
builder.Services.AddScoped<IWhatsAppService, WhatsAppService>();

//Stores jobs in sql server and starts a background worker within the app
builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHangfireServer();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.



 app.UseSwagger();
 app.UseSwaggerUI();



app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard();
RecurringJob.AddOrUpdate<IReminderService>(
    "send-pending-reminders",
    service => service.SendingPendingReminders(),
    Cron.Hourly);

RecurringJob.AddOrUpdate<IAssignmentService>(
    "mark-overdue-assignments",
    service => service.MarkOverdueAssignments(),
    Cron.Hourly);

app.MapControllers();

app.Run();
