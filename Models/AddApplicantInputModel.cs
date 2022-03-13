namespace DevJobs.API.Models
{
    public record AddApplicantInputModel(
        string Document, 
        string Name, 
        string Email,
        string UrlCurriculum
    )
    {
        
    }
}