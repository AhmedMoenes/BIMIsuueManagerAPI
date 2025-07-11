﻿namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IUserService _userService;

        public CompaniesController(ICompanyService companyService, IUserService userService)
        {
            _companyService = companyService;
            _userService = userService;
        }

        
        [HttpGet("")]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetAll()
        {
            IEnumerable<CompanyOverviewDto> companies = await _companyService.GetAllAsync();
            return Ok(companies);
        }

        [HttpGet("company-users/{companyId}")]
        public async Task<ActionResult<IEnumerable<UserOverviewDto>>> GetCompanyUsers(int companyId)
        {
            IEnumerable<CompanyUserDto> users = await _userService.GetCompanyUsers(companyId);
            return Ok(users);
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
        [Authorize(Roles = UserRoles.SuperAdmin)]
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

        [Authorize(Roles = UserRoles.CompanyAdmin)]
        [HttpGet("overview/{userId}")]
        public async Task<IActionResult> GetCompanyOverviewForUser(string userId)
        {
            IEnumerable<CompanyOverviewDto> companies = await _companyService.GetCompaniesForUserAsync(userId);
            return Ok(companies);
        }
    }
}
