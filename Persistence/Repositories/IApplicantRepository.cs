using DevJobs.API.Entities;

namespace DevJobs.API.Persistence.Repositories
{
    public interface IApplicantRepository
    {
         List<Applicant> GetAll();
         Applicant GetByDocument(string document);
         void Add(Applicant applicant);
         void AddSkill(ApplicantSkill skill);

    }
}