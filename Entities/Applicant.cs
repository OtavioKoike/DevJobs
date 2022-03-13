namespace DevJobs.API.Entities
{
    public class Applicant
    {
        // Atalho: ctrl + . e gerar contrutor com atributos selecionados
        public Applicant(string document, string name, string email, string urlCurriculum)
        {
            Document = document;
            Name = name;
            Email = email;
            UrlCurriculum = urlCurriculum;

            Skills = new List<ApplicantSkill>();
        }

        // Atalho: propg
        public int Id { get; private set; }
        public string Document { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string UrlCurriculum { get; private set; }
        public List<ApplicantSkill> Skills { get; private set; }
    }
}