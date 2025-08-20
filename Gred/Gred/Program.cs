using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.IO;
using gred.Data;
using Gred.PersistenceService;
using Microsoft.AspNetCore.Hosting;
using gred;

var builder = WebApplication.CreateBuilder(args);


// Add controllers
builder.Services.AddControllers();


builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins",builder =>
	{
		builder.AllowAnyOrigin()
        	.AllowAnyHeader()
		      .AllowAnyMethod();
	});
});
//var app=builder.Build();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
//
// Set up the database context
builder.Services.AddDbContext<GredDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddPersistanceService(builder.Configuration);

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
      options.RequireHttpsMetadata = false;  // Change to true for production to enforce HTTPS
      options.SaveToken = true;
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero  // No clock skew (strict expiration validation)
      };

      // Optional event for debugging token validation failures
      options.Events = new JwtBearerEvents
      {
        OnAuthenticationFailed = context =>
        {
          Console.WriteLine($"Authentication failed: {context.Exception.Message}");
          return Task.CompletedTask;
        }
      };
    });

// Enable Swagger and JWT Authorization
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gred API", Version = "v1" });

  // Add JWT Authorization header to Swagger
  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Enter 'Bearer <your_token>'"
  });

  c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

// CORS Configuration
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowLocalhost",
      policy => policy.WithOrigins("http://localhost:4200") // Adjust for your frontend URL
                      .AllowAnyHeader()
                      .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowLocalhost");

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();

var startup = new StartUp(builder.Configuration);
startup.Configure(app, app.Environment);

app.MapControllers();
app.Run();
