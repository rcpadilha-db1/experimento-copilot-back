using Experimento.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddValidators();
builder.Services.AddControllers();
builder.Services.AddMediator();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Experimento"
        });
    }
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1")
    );
}

app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync();