using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfireServer();
builder.Services.AddHangfire(x => x.UseSqlServerStorage("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HangfireDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard();//Implementa o dashboard
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
