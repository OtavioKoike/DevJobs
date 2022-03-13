namespace DevJobs.API.Models
{
    public record AddJobApplicationInputModel(
        string DocumentApplicant, 
        int IdJobVacancy
    )
    {
        
    }
}