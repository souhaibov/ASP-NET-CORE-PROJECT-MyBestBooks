﻿
using MyBestBooks.DataAccess.Data;
using MyBestBooks.DataAccess.Repository.IRepository;
using MyBestBooks.Models;

namespace MyBestBooks.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
