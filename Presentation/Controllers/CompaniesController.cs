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

        
        [HttpGet("")]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetAll()
        {
            IEnumerable<CompanyDto> companies = (IEnumerable<CompanyDto>)await _companyService.GetAllAsync();
            return Ok(companies);
        }

        
        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<ActionResult<CompanyDto>> GetById(int id)
        {
            CompanyDto company = await _companyService.GetByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        [HttpPost("create")]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<ActionResult> Create([FromBody] CreateCompanyDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdCompany = await _companyService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),              
                new { id = createdCompany.CompanyId }, 
                createdCompany               
            );
        }

        [HttpPost("create-with-admin")]
        //[Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<IActionResult> CreateWithAdmin([FromBody] CreateCompanyWithAdminDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _companyService.CreateCompanyWithAdminAsync(dto);
            return Ok(result);
        }

        [HttpPut("edit/{id}")]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateCompanyDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _companyService.UpdateAsync(id, dto);
            return NoContent();
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _companyService.DeleteAsync(id);
            return NoContent();
        }
        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpGet("overview")]
        public async Task<IActionResult> GetCompanyOverviewForUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var companies = await _companyService.GetCompaniesForUserAsync(userId);
            return Ok(companies);
        }
    }
}
