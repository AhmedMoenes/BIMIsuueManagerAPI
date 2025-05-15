namespace Infrastructure.Repositories
{
    public class LabelRepository : Repository<Label>, ILabelRepository
    {
        public LabelRepository(DbContext context) : base(context) { }
    }
}
