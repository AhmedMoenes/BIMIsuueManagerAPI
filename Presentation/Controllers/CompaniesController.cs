using Application.DTOs;
using Application.DTOs.Company;
using Application.Interfaces;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        //Get All Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetAll()
        {
            IEnumerable<CompanyDto> companies = await _companyService.GetAllAsync();
            return Ok(companies);
        }

        //Get Company By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> GetById(int id)
        {
            CompanyDto company = await _companyService.GetByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        //Create Company
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCompanyDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _companyService.CreateAsync(dto);
            // To Be Edited
            return StatusCode(201);
        }

        //UpdateAsync Company
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateCompanyDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _companyService.UpdateAsync(id, dto);
            return NoContent();
        }

        //DeleteAsync Company
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _companyService.DeleteAsync(id);
            return NoContent();
        }
    }
}
