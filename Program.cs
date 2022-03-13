using DevJobs.API.Persistence;
using DevJobs.API.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DevJobsCs");

// Injeção de dependencia: Transient, Scoped, Singleton
// Singleton: A mesma instancia sera usado em todo tempo de vida da aplicação
// Scoped: instancia é usada apenas para uma requisição
// Transient: Uma instancia por uso
// Com isso o DevJobsContext mantera o estado em todo tempo de vida da aplicação    
// builder.Services.AddSingleton<DevJobsContext>();
builder.Services.AddDbContext<DevJobsContext>(options => 
    options.UseInMemoryDatabase("DevJobs")
    // Transição de memoria para SQL Server
    // options.UseSqlServer(connectionString)
);

builder.Services.AddScoped<IJobVacancyRepository, JobVacancyRepository>();
builder.Services.AddScoped<IApplicantRepository, ApplicantRepository>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo{
        Title = "DevJobs.API",
        Version = "v1",
        Contact = new OpenApiContact{
            Name = "Otávio Koike",
            Email = "otaviokoike@hotmail.com",
            Url = new Uri("https://www.linkedin.com/in/otaviokoike")
        } 
    });

    var xmlFile = "DevJobs.API.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// builder.Host.ConfigureAppConfiguration((hostingContext, config) => {
//     Serilog.Log.Logger = new LoggerConfiguration()
//         .Enrich.FromLogContext()
//         .WriteTo.MSSqlServer(connectionString, sinkOptions: new MSSqlServerSinkOptions(){
//             AutoCreateSqlTable = true,
//             TableName = "Logs",
//         })
//         // .WriteTo.Console()
//         .CreateLogger();
// }).UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
