namespace MyBestBooks.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    { // here we will have all the repositories
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }

        void Save();

    }
}
// the implementation of how we will get the category repositary will be done in repository folder
