namespace Infrastructure.Repositories
{
    public class LabelRepository : Repository<Label>, ILabelRepository
    {
        public LabelRepository(AppDbContext context) : base(context) { }
    }
}
