using DevJobs.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevJobs.API.Persistence
{
    // Contexto de dados: Representa a classe que vai controlar/representar o contexto de dados da aplicação
    public class DevJobsContext: DbContext
    {
        // DbContextOptions será utilizado quando definir a confuguração na classe startup
        public DevJobsContext(DbContextOptions<DevJobsContext> options): base(options)
        {
        }

        // DbSet representa a tabela
        public DbSet<JobVacancy> JobVacancies { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        
        // Configurar nossa classe para virar tabela
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Configurações da tabela JobVacancy
            builder.Entity<JobVacancy>(e => {
                e.HasKey(jv => jv.Id);
                // e.ToTable("tb_JobVacancies");
                
                // Relacionamento
                e.HasMany(jv => jv.Applications)            // Um JobVacancy tem varias applications
                    .WithOne()                              // Uma application tem apenas uma vaga
                    .HasForeignKey(ja => ja.IdJobVacancy)   // Chave extrangeira: IdJobVacancy
                    .OnDelete(DeleteBehavior.Restrict);     // Delete Restrito
            });

            builder.Entity<JobApplication>(e => {
                e.HasKey(ja => ja.Id);
            });
        }
    }
}