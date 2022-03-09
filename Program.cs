using DevJobs.API.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DevJobsCs");

// Injeção de dependencia: Transient, Scoped, Singleton
// Singleton: A mesma instancia sera usado em todo tempo de vida da aplicação
// Scoped: instancia é usada apenas para uma requisição
// Transient: Uma instancia por uso
// Com isso o DevJobsContext mantera o estado em todo tempo de vida da aplicação    
// builder.Services.AddSingleton<DevJobsContext>();
builder.Services.AddDbContext<DevJobsContext>(options => 
    // options.UseInMemoryDatabase("DevJobs")
    // Transição de memoria para SQL Server
    options.UseSqlServer(connectionString)
);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
