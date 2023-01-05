using WebAppDecorator.Models;

namespace WebAppDecorator.Repositories
{
    public interface IProductRepository //IComponent
    {
        Task<Product> GetById(int id);
        Task<List<Product>> GetAll();
        Task<List<Product>> GetAll(string userId);
        Task<Product> Save(Product product);
        Task Update(Product product);
        Task Remove(Product product);



    }
}
