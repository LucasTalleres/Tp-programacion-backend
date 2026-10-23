using dao_library.entity_framework;
using Microsoft.EntityFrameworkCore;

namespace dao_library;

public abstract class EntityFrameworkDao<T> where T : class
{
    protected readonly AppDbContext Context;
    private readonly DbSet<T> records;
    private readonly Action<T, long> setId;

    protected EntityFrameworkDao(AppDbContext context, Action<T, long> setId)
    {
        Context = context;
        records = context.Set<T>();
        this.setId = setId;
    }

    public virtual IReadOnlyList<T> GetAll() => records.AsNoTracking().ToList();

    public virtual T? GetById(long id) => records.Find(id);

    public virtual T Save(T item)
    {
        records.Add(item);
        Context.SaveChanges();
        return item;
    }

    public virtual T? Update(long id, T item)
    {
        T? current = records.Find(id);
        if (current == null)
        {
            return null;
        }

        setId(item, id);
        Context.Entry(current).CurrentValues.SetValues(item);
        Context.SaveChanges();
        return current;
    }

    public bool DeleteById(long id)
    {
        T? item = records.Find(id);
        if (item == null)
        {
            return false;
        }

        records.Remove(item);
        Context.SaveChanges();
        return true;
    }
}
