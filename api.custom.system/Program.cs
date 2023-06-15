using api.custom.system.Service;
using api.custom.system.Service.Interfaces;
using api__custom_system.Config;
using api__custom_system.Controllers;
using api__custom_system.Repository;
using api__custom_system.Service;
using api__custom_system.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using MySql.EntityFrameworkCore.Extensions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserService, UserService>();

//Configura��o de CORS caso necess�rio
builder.Services.AddCors();

// Add services to the container.
builder.Services.AddControllersWithViews();
var key = Encoding.ASCII.GetBytes(Settings.Secret);
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(auth =>
    {
        auth.RequireHttpsMetadata = false;
        auth.SaveToken = true;
        auth.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(opts => opts.UseLazyLoadingProxies().UseMySQL(builder.Configuration.GetConnectionString("CadastroConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var provider = new FileExtensionContentTypeProvider();

provider.Mappings[".image"] = "image/jpg";

app.UseStaticFiles(new StaticFileOptions
{
    DefaultContentType = "image/jpg",
    ContentTypeProvider = provider
});


app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.WebRootPath,"images")),
    RequestPath = "/uploads"
});
//Configura��o de CORS caso necess�rio
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseRouting();



app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



app.Run();

internal class MyDesignTimeServices : IDesignTimeServices
{
    public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddEntityFrameworkMySQL();
        new EntityFrameworkRelationalDesignServicesBuilder(serviceCollection)
            .TryAddCoreServices();
    }

}
