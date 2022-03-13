// Atalho: api_controller
namespace DevJobs.API.Controllers
{
    using DevJobs.API.Entities;
    using DevJobs.API.Models;
    using DevJobs.API.Persistence.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;

    [Route("api/job-vacancies")]
    [ApiController]
    public class JobVacanciesController : ControllerBase
    {
        private readonly IJobVacancyRepository _repository;
        public JobVacanciesController(IJobVacancyRepository repository)
        {
            _repository = repository;
        }
        
        /// <summary>
        /// Buscar todas vagas de emprego
        /// </summary>
        /// <returns>Lista de vagas</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados Inválidos</response>
        [HttpGet]
        public IActionResult GetAll()
        {
            Log.Information("Buscando todas vagas cadastradsa");

            var jobVacancies = _repository.GetAll();
            return Ok(jobVacancies);
        }

        // Para ter um parametro id na rota
        // Ex.: api/job-vacancies/7
        /// <summary>
        /// Buscar uma vaga de emprego por Id
        /// </summary>
        /// <param name="id">Id da vaga buscada</param>
        /// <returns>Dados da vaga</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados Inválidos</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Log.Information($"Buscando Vaga {id}");
            
            var jobVacancy = _repository.GetById(id);
            
            if(jobVacancy == null)
                return NotFound();
            
            return Ok(jobVacancy);
        }

        /// <summary>
        /// Cadastrar uma vaga de emprego
        /// </summary>
        /// <remarks>
        /// {
        /// "title": "Estagio .Net",
        /// "description": "Ajudar a equipe a solucionar problemas utilizando o framework .Net",
        /// "company": "Banco PAN",
        /// "isRemote": true,
        /// "salaryRange": "2500 - 3000"
        /// }
        /// </remarks>
        /// <param name="model">Dados da vaga</param>
        /// <returns>Objeto recém criado</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Dados Inválidos</response>
        [HttpPost]
        public IActionResult Post(AddJobVacancyInputModel model)
        {
            Log.Information($"Criando Uma nova vaga - {model.Title}");

            var jobVacancy = new JobVacancy(
                model.Title, 
                model.Description, 
                model.Company, 
                model.IsRemote, 
                model.SalaryRange
            );

            _repository.Add(jobVacancy);

            return CreatedAtAction("GetById", new {id = jobVacancy.Id}, jobVacancy);
        }

        /// <summary>
        /// Atualizar uma vaga de emprego
        /// </summary>
        /// <remarks>
        /// {
        /// "title": ".Net Jr",
        /// "description": "Ajudar a equipe a solucionar problemas utilizando o framework .Net",
        /// }
        /// </remarks>
        /// <param name="id">Id da vaga a ser alterada</param>
        /// <param name="model">Dados da vaga</param>
        /// <response code="204">Sucesso</response>
        /// <response code="400">Dados Inválidos</response>
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateJobVacancyInputModel model)
        {
            Log.Information($"Atualizando vaga {id}");

            var jobVacancy = _repository.GetById(id);
            
            if(jobVacancy == null)
                return NotFound();

            jobVacancy.Update(model.Title, model.Description);
            _repository.Update(jobVacancy);
            
            return NoContent();
        }
    }
}