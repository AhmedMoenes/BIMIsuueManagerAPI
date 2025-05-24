namespace Domain.Entities
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
