using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SahabatAnak.Api.Data;
using SahabatAnak.Api.Repositories;
using SahabatAnak.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "SahabatAnak.Api", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IArticlesService, ArticlesService>();
builder.Services.AddScoped<IServicesService, ServicesService>();
builder.Services.AddScoped<ITeamsService, TeamsService>();
builder.Services.AddScoped<IReportsService, ReportsService>();
builder.Services.AddScoped<IContactsService, ContactsService>();
builder.Services.AddScoped<IDonationsService, DonationsService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IGalleryService, GalleryService>();
builder.Services.AddScoped<IVisitsService, VisitsService>();

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("spa", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            return;
        }

        policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "SahabatAnak";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "SahabatAnak";
var jwtKey = builder.Configuration["Jwt:Key"] ?? "DEV_ONLY_CHANGE_ME_TO_A_LONG_RANDOM_STRING";
var jwtKeyBytes = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtIssuer,
        ValidateAudience = true,
        ValidAudience = jwtAudience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(jwtKeyBytes),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(2),
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors("spa");
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await db.Database.EnsureCreatedAsync();
    await EnsureGalleryTableAsync(db);
    await EnsureArticleColumnsAsync(db);
    await EnsureVisitsTableAsync(db);
    await EnsureDonationsTableAsync(db);
    await EnsureDonationColumnsAsync(db);
    await DbInitializer.SeedAsync(db);
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static async Task EnsureGalleryTableAsync(ApplicationDbContext db)
{
    const string sql = """
IF OBJECT_ID(N'[GalleryPhotos]', N'U') IS NULL
BEGIN
    CREATE TABLE [GalleryPhotos] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Alt] nvarchar(max) NOT NULL,
        [ImageUrl] nvarchar(max) NOT NULL,
        [Category] nvarchar(max) NOT NULL,
        [SortOrder] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_GalleryPhotos] PRIMARY KEY ([Id])
    );
END
""";

    await db.Database.ExecuteSqlRawAsync(sql);
}

static async Task EnsureArticleColumnsAsync(ApplicationDbContext db)
{
    const string sql = """
IF COL_LENGTH('Articles', 'IsPublished') IS NULL
BEGIN
    ALTER TABLE [Articles] ADD [IsPublished] bit NOT NULL CONSTRAINT [DF_Articles_IsPublished] DEFAULT (1);
END
""";

    await db.Database.ExecuteSqlRawAsync(sql);
}

static async Task EnsureVisitsTableAsync(ApplicationDbContext db)
{
    const string sql = """
IF OBJECT_ID(N'[DailyVisits]', N'U') IS NULL
BEGIN
    CREATE TABLE [DailyVisits] (
        [Id] uniqueidentifier NOT NULL,
        [Day] date NOT NULL,
        [Count] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_DailyVisits] PRIMARY KEY ([Id])
    );
    CREATE UNIQUE INDEX [IX_DailyVisits_Day] ON [DailyVisits] ([Day]);
END
""";

    await db.Database.ExecuteSqlRawAsync(sql);
}

static async Task EnsureDonationsTableAsync(ApplicationDbContext db)
{
    const string sql = """
IF OBJECT_ID(N'[Donations]', N'U') IS NULL
BEGIN
    CREATE TABLE [Donations] (
        [Id] uniqueidentifier NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [Method] nvarchar(max) NOT NULL,
        [DonorName] nvarchar(max) NOT NULL,
        [DonorEmail] nvarchar(max) NOT NULL,
        [DonorPhone] nvarchar(max) NOT NULL,
        [RecipientBank] nvarchar(max) NOT NULL,
        [RecipientAccountNumber] nvarchar(max) NOT NULL,
        [RecipientAccountName] nvarchar(max) NOT NULL,
        [ReferenceCode] nvarchar(max) NOT NULL,
        [ProofUrl] nvarchar(max) NOT NULL,
        [Note] nvarchar(max) NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Donations] PRIMARY KEY ([Id])
    );
END
""";

    await db.Database.ExecuteSqlRawAsync(sql);
}

static async Task EnsureDonationColumnsAsync(ApplicationDbContext db)
{
    const string sql = """
IF COL_LENGTH('Donations', 'DonorPhone') IS NULL
BEGIN
    ALTER TABLE [Donations] ADD [DonorPhone] nvarchar(max) NOT NULL CONSTRAINT [DF_Donations_DonorPhone] DEFAULT ('');
END
IF COL_LENGTH('Donations', 'RecipientBank') IS NULL
BEGIN
    ALTER TABLE [Donations] ADD [RecipientBank] nvarchar(max) NOT NULL CONSTRAINT [DF_Donations_RecipientBank] DEFAULT ('');
END
IF COL_LENGTH('Donations', 'RecipientAccountNumber') IS NULL
BEGIN
    ALTER TABLE [Donations] ADD [RecipientAccountNumber] nvarchar(max) NOT NULL CONSTRAINT [DF_Donations_RecipientAccountNumber] DEFAULT ('');
END
IF COL_LENGTH('Donations', 'RecipientAccountName') IS NULL
BEGIN
    ALTER TABLE [Donations] ADD [RecipientAccountName] nvarchar(max) NOT NULL CONSTRAINT [DF_Donations_RecipientAccountName] DEFAULT ('');
END
IF COL_LENGTH('Donations', 'ReferenceCode') IS NULL
BEGIN
    ALTER TABLE [Donations] ADD [ReferenceCode] nvarchar(max) NOT NULL CONSTRAINT [DF_Donations_ReferenceCode] DEFAULT ('');
END
IF COL_LENGTH('Donations', 'ProofUrl') IS NULL
BEGIN
    ALTER TABLE [Donations] ADD [ProofUrl] nvarchar(max) NOT NULL CONSTRAINT [DF_Donations_ProofUrl] DEFAULT ('');
END
IF COL_LENGTH('Donations', 'Note') IS NULL
BEGIN
    ALTER TABLE [Donations] ADD [Note] nvarchar(max) NOT NULL CONSTRAINT [DF_Donations_Note] DEFAULT ('');
END
""";

    await db.Database.ExecuteSqlRawAsync(sql);
}
