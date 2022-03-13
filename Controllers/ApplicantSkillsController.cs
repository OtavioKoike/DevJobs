namespace ApplicantSkills.Controllers
{
    using DevJobs.API.Entities;
    using DevJobs.API.Models;
    using DevJobs.API.Persistence.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;

    [Route("api/applicants/{document}/skills")]
    [ApiController]
    public class ApplicantSkillsController : ControllerBase
    {
        private readonly IApplicantRepository _repository;

        public ApplicantSkillsController(IApplicantRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Cadastrar uma Skill em um candidato existente
        /// </summary>
        /// <remarks>
        /// {
        /// "description": ".Net",
        /// "time": "1 ano",
        /// "documentApplicant": "12345678900"
        /// }
        /// </remarks>
        /// <param name="document">Documento do candidado a ser inserido</param>
        /// <param name="model">Dados da Skill</param>
        /// <response code="204">Sucesso</response>
        /// <response code="400">Dados Inválidos</response>
        [HttpPost]
        public IActionResult Post(string document, AddApplicantSkillInputModel model)
        {
            Log.Information($"Cadastrando uma nova Skill para o candidato {document}");

            var applicant = _repository.GetByDocument(document);

            if(applicant == null)
                return NotFound();

            var applicantSkill = new ApplicantSkill(
                model.Description,
                model.Time,
                applicant.Id
            );

            _repository.AddSkill(applicantSkill);

            return NoContent();
        }
    }
}