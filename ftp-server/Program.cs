using ftp_server.Services;
using ftp_server.Services.Abstracts;
using ftp_server.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new() { Title="File Manager API", Version="v1" });
});

builder.Services.AddScoped<IFileManagerService, FileManagerService>();
builder.Services.AddCors(options => 
{
    options.AddPolicy("Policy1", builder => 
    {
        builder.WithOrigins("http://localhost:3000")
               .WithMethods("POST", "GET", "OPTION");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
}

app.UseCors("Policy1");

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
