namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyProjectController : ControllerBase
    {
        private readonly ICompanyProjectService _companyProjectService;
        public CompanyProjectController(ICompanyProjectService companyProjectService)
        {
            _companyProjectService = companyProjectService;
        }

        [HttpPost("assign-companies")]
        public async Task<IActionResult> AssignCompanies([FromBody] AssignCompaniesToProjectDto dto)
        {
            await _companyProjectService.AssignCompaniesAsync(dto);
            return Ok();
        }

        [HttpGet("overview/company/{companyId}")]
        //[Authorize(Roles = UserRoles.CompanyAdmin)]
        public async Task<IActionResult> GetForCompany(int companyId)
        {
            var result = await _companyProjectService.GetForCompanyAsync(companyId);
            return Ok(result);
        }
    }
}
