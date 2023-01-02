using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ScriptShoesAPI.Database;
using ScriptShoesAPI.Features.AdminPanel.Commands.AddShoe;
using ScriptShoesAPI.Features.Users.Commands.CreateUser;
using ScriptShoesAPI.Features.Users.Queries.Login;
using ScriptShoesAPI.Features.Users.Tokens;
using ScriptShoesAPI.Middleware;
using ScriptShoesAPI.Models.Reviews;
using ScriptShoesAPI.PipelineBehaviors;
using ScriptShoesAPI.Requests;
using ScriptShoesAPI.Services.DiscordLogger;
using ScriptShoesAPI.Services.EmailSender;
using ScriptShoesAPI.Services.UserContext;
using ScriptShoesAPI.Validators.AdminPanelValidators;
using ScriptShoesAPI.Validators.ReviewsValidators;
using ScriptShoesAPI.Validators.UsersValidators;
using ScriptShoesCQRS.Features.AdminPanel.Commands.UpdateShoe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddDbContext<AppDbContext>(config  =>
{
    var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:connectionString");
    config.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
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
        builder
            .AllowAnyHeader()
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
//app.UseMiddleware<ErrorHandlingMiddleWare>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.RegisterAccountEndpoints();
app.RegisterAdminPanelEndpoints();
app.RegisterCartEndpoints();
app.RegisterReviewsEndpoints();
app.RegisterFavoritesEndpoints();
app.RegisterShoesEndpoints();

app.Run();
