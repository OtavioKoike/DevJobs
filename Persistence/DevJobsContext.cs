using DevJobs.API.Entities;

namespace DevJobs.API.Persistence
{
    // Contexto de dados: Representa a classe que vai controlar/representar o contexto de dados da aplicação
    public class DevJobsContext
    {
        
        public DevJobsContext()
        {
            JobVacancies = new List<JobVacancy>();
        }
        public List<JobVacancy> JobVacancies { get; set; }
        
    }
}