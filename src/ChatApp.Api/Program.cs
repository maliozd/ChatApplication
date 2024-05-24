using ChatApp.Api.Extensions;
using ChatApp.Application;
using ChatApp.SignalR;
using ChatApp.SignalR.Hubs;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Chat API", Version = "v1" });
    option.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddSignalR(options =>
{
    options.KeepAliveInterval = TimeSpan.FromSeconds(5);
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(config =>
    {
        config.AllowAnyHeader().
        AllowAnyOrigin().
        AllowAnyMethod().Build();
    });
    options.AddPolicy("default", config =>
    {
        config.AllowAnyHeader().
        AllowAnyOrigin().
        AllowAnyMethod().Build();
    });
});
builder.Services.AddAuthentincationConfiguration(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplication();
builder.Services.RegisterSignalR();
builder.Services.AddInfrastructureServices(builder.Configuration);


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseHttpsRedirection();
app.UseCors(configurePolicy: config => config.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader().Build());
app.UseCors("default");

app.UseAuthentication();


//app.MapControllers();
app.UseRouting().
    UseAuthorization().
    UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<MessageHub>("hubs/messagehub");
});
app.Run();
