namespace Domain.Entities
{
    public class Area
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        
        public ICollection<Issue> Issues { get; set; }
    }
}
