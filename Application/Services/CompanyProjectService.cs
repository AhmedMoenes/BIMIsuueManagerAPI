namespace Application.Services
{
    public class CompanyProjectService : ICompanyProjectService
    {
        private readonly ICompanyProjectRepository _repo;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyProjectService(ICompanyProjectRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }


        public async Task AssignCompaniesAsync(AssignCompaniesToProjectDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                await _repo.AssignCompaniesToProjectAsync(dto.ProjectId, dto.CompanyIds);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
