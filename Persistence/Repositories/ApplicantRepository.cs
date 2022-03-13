using DevJobs.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevJobs.API.Persistence.Repositories
{
    public class ApplicantRepository : IApplicantRepository
    {
        private readonly DevJobsContext _context;

        public ApplicantRepository(DevJobsContext context)
        {
            _context = context;
        }

        public void Add(Applicant applicant)
        {
            _context.Applicants.Add(applicant);
            // Realmente persiste no banco de dados
            _context.SaveChanges();
        }

        public void AddSkill(ApplicantSkill skill)
        {
            _context.ApplicantSkills.Add(skill);
            _context.SaveChanges();
        }

        public List<Applicant> GetAll()
        {
            return _context.Applicants.ToList();
        }

        public Applicant GetByDocument(string document)
        {
            return _context.Applicants
                .Include(app => app.Skills) // Forçar o carregamento das informações
                .SingleOrDefault(app => app.Document.Equals(document));
        }

    }
}