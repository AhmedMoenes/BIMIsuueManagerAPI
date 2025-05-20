namespace Domain.Entities
{
    public class Area
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public ICollection<Issue> Issues { get; set; }
    }
}
