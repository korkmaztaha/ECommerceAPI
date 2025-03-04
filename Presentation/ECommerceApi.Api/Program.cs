using ECommerceApi.Api.Filters;
using ECommerceApi.Application.Validators.Products;
using ECommerceApi.Infrastructure;
using ECommerceApi.Infrastructure.Filters;
using ECommerceApi.Infrastructure.Services.Storage.Azure;
using ECommerceApi.Infrastructure.Services.Storage.Local;
using ECommerceApi.Persistence;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4100", "https://localhost:4100").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));


builder.Services.AddPersitenceServices();
builder.Services.AddInfrastructureServices();
//builder.Services.AddStorage<LocalStorage>();
//TODO: detaylý test et
builder.Services.AddStorage<AzureStorage>();
builder.Services.AddControllers(options =>options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(conf=>conf.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options=>options.SuppressModelStateInvalidFilter=true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<SwaggerFileUploadOperationFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();


app.MapControllers();

app.Run();
