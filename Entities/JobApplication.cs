namespace DevJobs.API.Entities
{
    public class JobApplication
    {
        // Atalho: ctrl + . e gerar contrutor com atributos selecionados
        public JobApplication(string applicantName, string applicantEmail, int idJobVacancy)
        {
            ApplicantName = applicantName;
            ApplicantEmail = applicantEmail;
            IdJobVacancy = idJobVacancy;
        }

        // Atalho: propg
        public int Id { get; private set; }
        public string ApplicantName { get; private set; }
        public string ApplicantEmail { get; private set; }
        public int IdJobVacancy { get; private set; }
        
    }
}