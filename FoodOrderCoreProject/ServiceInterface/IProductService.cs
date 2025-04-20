using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrderCoreProject.DTOs;
namespace FoodOrderCoreProject.ServiceInterface
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllProducts();
        Task<ProductDTO> GetProductByName(string name);
        Task<ProductDTO> AddNewProduct(ProductDTO product);
        Task<ProductUpdateDTO> UpdateAProduct(ProductUpdateDTO product);
        Task<bool> DeleteProduct(string productName);
    }
}
