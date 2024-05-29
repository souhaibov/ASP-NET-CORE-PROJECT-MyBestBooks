using MyBestBooks.Models;

namespace MyBestBooks.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    // this interface will implement the IRepository interface and this time when it implements the IRepository we know what's the class on which we want the implementation for this repository
    {
        void Update(Category obj);

    }
}

// this is the final CategoryRepository interface that will implements the base fonctionality that we have for all the repository plus we have an update and save