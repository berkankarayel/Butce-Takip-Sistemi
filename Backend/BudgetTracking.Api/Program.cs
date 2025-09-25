using BudgetTracking.Application.Interfaces;
using BudgetTracking.Application.Services; // AuthService burada kalıyor
using BudgetTracking.Domain.Entities;
using BudgetTracking.Infrastructure.Data;
using BudgetTracking.Infrastructure.Services; // ✅ ExpenseService ve IncomeService buradan gelecek
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1) PostgreSQL bağlantısı
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2) Identity ayarları
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// 2.2) JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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

// 3) Servis katmanı bağımlılıkları
builder.Services.AddScoped<IAuthService, AuthService>(); // Application’da
builder.Services.AddScoped<IExpenseService, ExpenseService>(); // Infrastructure’da
builder.Services.AddScoped<IIncomeService, IncomeService>();   // Infrastructure’da

// 4) AutoMapper
builder.Services.AddAutoMapper(typeof(BudgetTracking.Application.Profiles.MappingProfile).Assembly);

// 5) Controller & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BudgetTracking.Api", Version = "v1" });

    // 🔑 JWT Authentication ekle
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header kullanın. \r\n\r\n 'Bearer {token}' şeklinde girin."
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

var app = builder.Build();

// 6) Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();  // ✅ önce kimlik doğrulama
app.UseAuthorization();   // ✅ sonra yetkilendirme

app.MapControllers();     // ✅ controller bazlı endpointler aktif

// 7) Varsayılan roller ve admin kullanıcı seed edilsin
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    string[] roles = new[] { "Admin", "User" };

    // Roller oluştur
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Admin kullanıcı bilgileri
    string adminUserName = "admin";
    string adminEmail = "admin@budget.com";
    string adminPassword = "Admin123!";
    string adminFullName = "Sistem Admin";

    // Admin yoksa ekle
    var existingAdmin = await userManager.FindByNameAsync(adminUserName);
    if (existingAdmin == null)
    {
        var admin = new AppUser
        {
            UserName = adminUserName,
            Email = adminEmail,
            FullName = adminFullName,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(admin, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}

app.Run();
