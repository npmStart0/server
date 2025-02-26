using BLL.Interfaces;
using BLL.Services;
using BLL.Validations;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using dotenv.net;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
Env.Load("../.env.local");
#endif

string clientUrl = Env.GetString("CLIENT_URL");

// Configure DbContext
builder.Services.AddDbContext<MyDbContext>();


// Configure DbContext
builder.Services.AddDbContext<MyDbContext>();

// Configure repositories and services
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IDiscussionRepository, DiscussionRepository>();
builder.Services.AddTransient<IDiscussionService, DiscussionService>();
builder.Services.AddTransient<ISubjectRepository, SubjectRepository>();
builder.Services.AddTransient<ISubjectService, SubjectService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();

// Add UserValidations
builder.Services.AddTransient<UserValidations>();
builder.Services.AddTransient<SubjectValidations>();
builder.Services.AddTransient<DiscussionValidations>();



builder.Services.AddTransient<IContext, MyDbContext>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder => {
            builder.WithOrigins(clientUrl);
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        }
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "server is running");

app.Run();