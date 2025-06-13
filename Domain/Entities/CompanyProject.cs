namespace Domain.Entities
{
    public class CompanyProject
    {
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
