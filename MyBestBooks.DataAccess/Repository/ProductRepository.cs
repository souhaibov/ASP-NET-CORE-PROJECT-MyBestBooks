
using MyBestBooks.DataAccess.Data;
using MyBestBooks.DataAccess.Repository.IRepository;
using MyBestBooks.Models;

namespace MyBestBooks.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}
