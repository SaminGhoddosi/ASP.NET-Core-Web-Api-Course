using Microsoft.EntityFrameworkCore;
using WebApplication1.Contracts;
using WebApplication1.Data;
using WebApplication1.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApplication1.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Serilog;
using WebApplication1.Middlewares;

var builder = WebApplication.CreateBuilder(args);//o que colocar no v1.

// Add services to the container.
var logger = new LoggerConfiguration()
    .WriteTo.Console() //Os logs vão ser enviados para o Console
    .WriteTo.File("Logs/NzWaks_Log.txt", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Information() // define o nível mínimo de log que será mostrado
    .CreateLogger();

builder.Logging.ClearProviders(); //limpa os provedores padrão do ssit
builder.Logging.AddSerilog(logger);


builder.Services.AddControllers();
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "NZ Walks API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme
            },
            Scheme = "Oauth2",
            Name = JwtBearerDefaults.AuthenticationScheme,
            In = ParameterLocation.Header
        },
        new List<string>()
        }
    });
});

builder.Services.AddDbContext<NZWalksDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));
builder.Services.AddDbContext<NZWalksAuthDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksAuthConnectionString")));
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();
builder.Services.AddScoped<IDifficultyRepository, SQLDifficultyRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
//adiciona authentication e usa jwt como sistema de autenticação
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//adicionar as instruções
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        //validar quem fez o token
        ValidateIssuer = true,
        //validar se o token é válido para esta api dentro das apis do sistema
        ValidateActor = true,
        //verificar validade
        ValidateLifetime = true,//.configuration é pegar lá do json settings
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        //symetric = mesma chave para criptografar e descriptografar
        //Encoding.UTF8.GetBytes = converter o texto para bytes, que é usado para criptografia
    });
builder.Services.AddIdentityCore<IdentityUser>().AddRoles<IdentityRole>().AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NzWalks").AddEntityFrameworkStores<NZWalksAuthDBContext>()
    .AddDefaultTokenProviders(); //addiciona tokens padrões(como email, phonenumber)       //DataProtector                          //nome único para identificar o meu provedor

//senha do usuário //usar o option quando for configurar o JWT
builder.Services.Configure<IdentityOptions>(options =>
{                         //classe que vai definir como o sistema de identidade vai se comportar
    options.Password.RequireDigit = false;//números
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;//símbolos
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHttpsRedirection();
//Autenticar antes de autorizar
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


