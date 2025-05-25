namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly AppDbContext Context;
        public readonly DbSet<T> DbSet;
        public Repository(AppDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await DbSet.ToListAsync();

        public async Task<T> GetByIdAsync(object id) => await DbSet.FindAsync(id);

        public async Task<T> AddAsync(T entity)
        {
            var result = await DbSet.AddAsync(entity);
            return result.Entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            var list = entities.ToList(); 
            await DbSet.AddRangeAsync(list);
            return list;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            DbSet.Update(entity);
            return await Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                return false;

            DbSet.Remove(entity);
            return await Context.SaveChangesAsync() > 0;
        }

        public async Task SaveChangesAsync() => await Context.SaveChangesAsync();
    }
}
