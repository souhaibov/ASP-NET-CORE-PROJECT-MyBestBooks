
using MyBestBooks.DataAccess.Data;
using MyBestBooks.DataAccess.Repository.IRepository;

namespace MyBestBooks.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork  // UnitOfWork is a public class that will implement the IUnitOfWork interface
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
