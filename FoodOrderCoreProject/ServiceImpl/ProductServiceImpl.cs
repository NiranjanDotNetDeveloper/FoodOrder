using FoodOrderCoreProject.Domain.Entities;
using FoodOrderCoreProject.Domain.RepositoryInterfaces;
using FoodOrderCoreProject.DTOs;
using FoodOrderCoreProject.ServiceInterface;

namespace FoodOrderCoreProject.ServiceImpl
{
    public class ProductServiceImpl:IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductServiceImpl(IProductRepository productRepo)
        {
            _productRepository = productRepo;
        }
        public async Task<ProductDTO> AddNewProduct(ProductDTO product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            else
            {
                await _productRepository.AddNewProduct(product.ConvertProductDTOToProduct());
                return product;
            }

        }

        public async Task<bool> DeleteProduct(string productName)
        {
            if (productName == null)
            {
                throw new ArgumentNullException("Products not found");
            }
            else
            {
                bool isProductDeleted = await _productRepository.DeleteProduct(productName);
                return isProductDeleted;
            }   
        }

        public async Task<List<ProductDTO>> GetAllProducts()
        {
            var listOfProducts = await _productRepository.GetAllProducts();
            var listOfProductDTOs = listOfProducts.Select(x => x.ConvertProductToProductDTO()).ToList();
            if (listOfProductDTOs.Count <= 0)
            {
                throw new ArgumentNullException("Products not found");
            }
            else
            {
                return listOfProductDTOs;
            }
        }

        public async Task<ProductDTO> GetProductByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            else
            {
                var product = await _productRepository.GetProductByName(name);
                return product.ConvertProductToProductDTO();
            }
        }
        public async Task<ProductUpdateDTO> UpdateAProduct(ProductUpdateDTO product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            else
            {
                await _productRepository.UpdateAProduct(product.ConvertProductToProductUpdateDTO());
                return product;
            }
        }
    }
}
