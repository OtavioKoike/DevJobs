namespace Applicants.Controllers
{
    using DevJobs.API.Entities;
    using DevJobs.API.Models;
    using DevJobs.API.Persistence.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;

    [Route("api/applicants")]
    [ApiController]
    public class ApplicantsController : ControllerBase
    {
        private readonly IApplicantRepository _repository;

        public ApplicantsController(IApplicantRepository repository)
        {
            _repository = repository;
        }
        
        /// <summary>
        /// Buscar todos candidatos
        /// </summary>
        /// <returns>Lista de candidatos</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados Inválidos</response>
        [HttpGet]
        public IActionResult GetAll()
        {
            Log.Information("Buscando todos candidatos");
            
            var applicants = _repository.GetAll();
            return Ok(applicants);
        }

        /// <summary>
        /// Buscar um candidato pelo documento
        /// </summary>
        /// <param name="document">Documento do candidato a ser buscado</param>
        /// <returns>Dados do candidato</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados Inválidos</response>
        [HttpGet("{document}")]
        public IActionResult GetByDocument(string document)
        {
            Log.Information($"Buscando candidato pelo seu documento {document}");
            var applicant = _repository.GetByDocument(document);
            
            if(applicant == null)
                return NotFound();
            
            return Ok(applicant);
        }

        /// <summary>
        /// Cadastrar um candidato
        /// </summary>
        /// <remarks>
        /// {
        /// "document": "12345678900",
        /// "name": "Otavio",
        /// "email": "otaviokoike@hotmail,com",
        /// "urlCurriculum": "www.linkedin.com/in/otaviokoike"
        /// }
        /// </remarks>
        /// <param name="model">Dados do candidato</param>
        /// <returns>Objeto recém criado</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Dados Inválidos</response>
        [HttpPost]
        public IActionResult Post(AddApplicantInputModel model)
        {
            Log.Information($"Cadastrando o candidato {model.Document}");

            var applicant = new Applicant(
                model.Document, 
                model.Name, 
                model.Email, 
                model.UrlCurriculum 
            );

            _repository.Add(applicant);

            return CreatedAtAction("GetByDocument", new {document = applicant.Document}, applicant);
        }
    }
}