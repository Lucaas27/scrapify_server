using Hangfire;
using scrapify.API.Configuration;
using scrapify.API.Interfaces;
using scrapify.API.Jobs;
using scrapify.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IJobs, JobScheduler>();
builder.AddHangfireService();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.ConfigureHangfireJobs();

await app.RunAsync();