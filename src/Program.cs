using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ScriptShoesCQRS.Database;
using ScriptShoesCQRS.Features.AdminPanel.AdminPanelValidators;
using ScriptShoesCQRS.Features.AdminPanel.Commands.AddShoe;
using ScriptShoesCQRS.Features.AdminPanel.Commands.UpdateShoe;
using ScriptShoesCQRS.Features.Reviews.Commands.UpdateReview;
using ScriptShoesCQRS.Features.Reviews.ReviewsValidators;
using ScriptShoesCQRS.Features.Users;
using ScriptShoesCQRS.Features.Users.Commands.CreateUser;
using ScriptShoesCQRS.Features.Users.Queries.Login;
using ScriptShoesCQRS.Features.Users.Tokens;
using ScriptShoesCQRS.Features.Users.UsersValidators;
using ScriptShoesCQRS.Middleware;
using ScriptShoesCQRS.Models.Reviews;
using ScriptShoesCQRS.PipelineBehaviors;
using ScriptShoesCQRS.Services.DiscordLogger;
using ScriptShoesCQRS.Services.EmailSender;
using ScriptShoesCQRS.Services.UserContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddDbContext<AppDbContext>(config  =>
{
    config.UseSqlServer(builder.Configuration.GetValue<string>("ConnectionStrings:connectionString"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddFluentValidation(fv => fv.AutomaticValidationEnabled = true);
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo() { Title = "ScriptShoes API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Description = "Enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddCors(setup =>
{
    setup.AddPolicy("ui", builder =>
    {
        builder.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient<IDiscordLoggerService, DiscordLoggerService>();
builder.Services.AddScoped<ITokensMethods, TokensMethods>();
builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<ErrorHandlingMiddleWare>();


builder.Services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
builder.Services.AddScoped<IValidator<LoginQuery>, LoginQueryValidator>();
builder.Services.AddScoped<IValidator<AddShoeCommand>, AddShoeValidator>();
builder.Services.AddScoped<IValidator<UpdateShoeCommand>, UpdateShoeValidator>();
builder.Services.AddScoped<IValidator<CreateReviewDto>, CreateReviewCommandValidator>();
builder.Services.AddScoped<IValidator<UpdateReviewDto>, UpdateReviewCommandValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("ui");
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScriptShoes API"); });
app.UseMiddleware<ErrorHandlingMiddleWare>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();