namespace DTOs.CompanyProject
{
    public class AssignCompaniesToProjectDto
    {
        public int ProjectId { get; set; }
        public List<int> CompanyIds { get; set; }
    }
}
