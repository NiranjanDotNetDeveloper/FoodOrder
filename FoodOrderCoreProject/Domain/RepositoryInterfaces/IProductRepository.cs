using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrderCoreProject.Domain.Entities;
using FoodOrderCoreProject.DTOs;
namespace FoodOrderCoreProject.Domain.RepositoryInterfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductByName(string name);
        Task<Product> AddNewProduct(Product product);
        Task<Product> UpdateAProduct(Product product);
        Task<bool> DeleteProduct(string productName);
    }
}
