namespace DevJobs.API.Entities
{
    public class ApplicantSkill
    {
        // Atalho: ctrl + . e gerar contrutor com atributos selecionados
        public ApplicantSkill(string description, string time, int idApplicant)
        {
            Description = description;
            Time = time;
            IdApplicant = idApplicant;
        }

        // Atalho: propg
        public int Id { get; private set; }
        public string Description { get; private set; }
        public string Time { get; private set; }
        public int IdApplicant { get; private set; }
        
    }
}