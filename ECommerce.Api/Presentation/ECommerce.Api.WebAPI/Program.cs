using ECommerce.Api.Application;
using ECommerce.Api.Application.Validators.ProductValidators;
using ECommerce.Api.Infrastructure;
using ECommerce.Api.Infrastructure.Filters;
using ECommerce.Api.Infrastructure.Services.Storage.Azure;
using ECommerce.Api.Infrastructure.Services.Storage.Local;
using ECommerce.Api.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.PersistenceServiceRegistrations();
builder.Services.InfrastructureServiceRegistrations();
builder.Services.ApplicationServiceRegistrations();

builder.Services.AddStorage<AzureStorage>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:44328/", "http://localhost:44328/")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
builder.Services.AddControllers();
#region fluentvalidation

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
//Eski kod (Yenisi test edilmedi !!!)
//builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
//    .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
//    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin",options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, //Oluşturulacak token değerini kimlerin/hangi originlerin/sitelerin kullanacağını belittiğimiz değerdir.
            ValidateIssuer = true,//Oluşturulacak token değerini kimlerin/hangi originlerin/sitelerin dağıttığını belittiğimiz değerdir.
            ValidateLifetime = true,//Oluşturulacak token değerini süresini kontrol edecek doğrulamadır.
            ValidateIssuerSigningKey = true,//Oluşturulacak token değerini uygulamamıza ait bir değer olduğunu ifade eden security key verisinin doğrulanmasıdır.

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
