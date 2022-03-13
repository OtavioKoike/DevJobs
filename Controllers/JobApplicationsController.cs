namespace DevJobs.API.Controllers
{
    using DevJobs.API.Entities;
    using DevJobs.API.Models;
    using DevJobs.API.Persistence.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;

    [Route("api/job-vacancies/{id}/applications")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobVacancyRepository _repositoryVacancy;
        private readonly IApplicantRepository _repositoryApplicanty;
        public JobApplicationsController(
            IJobVacancyRepository repositoryVacancy,
            IApplicantRepository repositoryApplicant    
        )
        {
            _repositoryVacancy = repositoryVacancy;
            _repositoryApplicanty = repositoryApplicant;
        }
        
        /// <summary>
        /// Cadastrar em uma vaga de emprego existente
        /// </summary>
        /// <remarks>
        /// {
        /// "documentApplicant": "12345678900",
        /// "idJobVacancy": 1
        /// }
        /// </remarks>
        /// <param name="id">Id da vaga a ser aplicada</param>
        /// <param name="model">Dados da Pessoa</param>
        /// <response code="204">Sucesso</response>
        /// <response code="400">Dados Inválidos</response>
        [HttpPost]
        public IActionResult Post(int id, AddJobApplicationInputModel model)
        {
            Log.Information($"Cadastrando uma nova aplicação na vaga {id} pelo candidato {model.DocumentApplicant}");

            var jobVacancy = _repositoryVacancy.GetById(id);
            if(jobVacancy == null)
                return NotFound("Não foi encontrada a Vaga");

            var applicant = _repositoryApplicanty.GetByDocument(model.DocumentApplicant);
            if(applicant == null)
                return NotFound("Não foi encontrado o Candidato");

            var application = new JobApplication(
                applicant.Id, 
                id);
                
            _repositoryVacancy.AddApplication(application);
            
            return NoContent();
        }
    }
}