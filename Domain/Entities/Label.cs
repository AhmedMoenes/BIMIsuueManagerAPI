﻿namespace Domain.Entities
{
    public class Label
    {
        public int LabelId { get; set; }
        public string LabelName { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public ICollection<IssueLabel> Issues { get; set; }
    }
}
