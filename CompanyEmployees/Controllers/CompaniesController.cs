using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public CompaniesController(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult GetCompanies()
        {
            try
            {
                var companies = _repository.Company.GetAllCompanies(trackChanges: false);
                var companiesDto = companies.Select(x => new CompanyDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    FullAddress = string.Join(' ',x.Address,x.Country)
                }).ToList();
                return Ok(companiesDto);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetCompanies)}action { exception}");
            return StatusCode(500, "Internal server error");
            }
        }
    }
}
