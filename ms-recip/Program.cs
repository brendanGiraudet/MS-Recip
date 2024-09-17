using ms_recip.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddDatabaseContext(builder.Configuration);
builder.Services.AddODataContext();
builder.Services.AddCustomServices();
builder.Services.AddRepositories();
builder.Services.AddCustomHostedServices();
builder.Services.AddSettings(builder.Configuration);

var app = builder.Build();

// Apply migrations
app.ApplyDatabaseMigrations();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints => { _ = endpoints.MapControllers(); });

app.Run();
