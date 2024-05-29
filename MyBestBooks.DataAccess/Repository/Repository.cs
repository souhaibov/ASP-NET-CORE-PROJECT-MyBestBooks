using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyBestBooks.DataAccess.Data;
using MyBestBooks.DataAccess.Repository.IRepository;

namespace MyBestBooks.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            // when we create this generic class (T) on category... the dbSet will be set to Categories
            //_db.Categories == dbSet the _db.Categories is equivalent to the dbset here
            // instead of putting _db.Categories.Add() now all we do is to say dbSet.Add()
            _db.Products.Include(u => u.Category); // we can put many includes in the same line
            // _db.Products.Include(u => u.Category).Include(u=> u.Name).Include(u=> u.CategoryId)
            // that's to put our categories to the _db and then we will can call it into the GetAll IEnumerable
            // to use to show it in the category column in the index
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet; // basically right now we have the complete dbSet that we have assign here
            query = query.Where(filter);
			// Get must always return one entity
			if (!string.IsNullOrEmpty(includeProperties)) // exactly like in the GetAll
			{
				foreach (var includeProp in includeProperties
					.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			return query.FirstOrDefault();   // same as we did in the third condition in Categories... that's the same logic that we are implemented right here
        }

        //Category, Name,.... (CategoryId)...
        //something important is that the name here (Category for example) should be exactly the same called in the include
        // we should add it in the Interface Repository (IRepository) as well
        public IEnumerable<T> GetAll(string? includeProperties = null) //to say that if someone gives us category or name or categoryId based on that we can build the includeProperties
        {
            IQueryable<T> query = dbSet; // dbSet will basically have all the records like Categories in our example... we have to get back with the return
            
            if(!string.IsNullOrEmpty(includeProperties)) // if it's not null or empty
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                   query = query.Include(includeProp);
                }
            }

            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity); // it will remove all the categories that we have
        }
    }
}

// with that our repository interface has been implemented and this looks much cleaner
