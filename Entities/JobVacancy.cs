namespace DevJobs.API.Entities
{
    public class JobVacancy
    {
        public JobVacancy(string title, string description, string company, bool isRemote, string salaryRange)
        {
            Title = title;
            Description = description;
            Company = company;
            IsRemote = isRemote;
            SalaryRange = salaryRange;

            CreatAt = DateTime.Now;
            Applications = new List<JobApplication>();
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Company { get; private set; }
        public bool IsRemote { get; private set; }
        public string SalaryRange { get; private set; }
        public DateTime CreatAt { get; private set; }
        public List<JobApplication> Applications { get; private set; }
        
        // Todos atributos possuem set private, logo só poderão ser alterados pela função update
        public void Update(string title, string description) {
            Title = title;
            Description = description;
        }
        
    }
}