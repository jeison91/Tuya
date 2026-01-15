using Tuya.Api.DI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

DependencyInjection.AddRegistration(builder.Services, builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Tuya v1");
    });
}

app.UseHttpsRedirection();
app.UseCors("politica");
//app.UseMiddleware(typeof(ExceptionMiddleware));

app.UseAuthorization();

app.MapControllers();

app.Run();
