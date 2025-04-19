using FoodInfrastructure.DbContextClass;
using FoodOrderCoreProject.Domain.Entities;
using FoodOrderCoreProject.Domain.RepositoryInterfaces;
using FoodOrderCoreProject.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodInfrastructure.RepositoryImpl
{
    public class ProductRepositoryImpl : IProductRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ProductRepositoryImpl(ApplicationDbContext dbContext)
        {
            _applicationDbContext = dbContext;
        }
        public async Task<ProductDTO> AddNewProduct(ProductDTO product)
        {
            if (product == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                ProductDTO productDTO = new ProductDTO();
                productDTO.ProductName = product.ProductName;
                productDTO.Price = product.Price;
                productDTO.ProductDescription = product.ProductDescription;
                productDTO.CategoryId = product.CategoryId;
                await _applicationDbContext.Products.AddAsync(productDTO.ConvertProductDTOToProduct());
                await _applicationDbContext.SaveChangesAsync();
                return productDTO;
            }
            
        }

        public async Task<bool> DeleteProduct(string productName)
        {
            bool isProductDeleted = false;
            ProductDTO? productToBeDeleted = await _applicationDbContext.Products.Select(x=>x.ConvertProductToProductDTO()).FirstOrDefaultAsync(x => x.ProductName == productName);
            if (productToBeDeleted == null)
            {
                throw new ArgumentNullException("Products not found");
            }
            else
            {
                _applicationDbContext.Products.Remove(productToBeDeleted.ConvertProductDTOToProduct());
                await _applicationDbContext.SaveChangesAsync();
                isProductDeleted = true;
            }
            return isProductDeleted;
        }

        public async Task<List<ProductDTO>> GetAllProducts()
        {
            List<ProductDTO>listOfDTO= await _applicationDbContext.Products.Select(x=>x.ConvertProductToProductDTO()).ToListAsync();
            if (listOfDTO.Count <= 0) {
                throw new ArgumentNullException("Products not found");
            }
            else
            {
                return listOfDTO;
            }
        }

        public async Task<ProductDTO> GetProductByName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new NotImplementedException();
            }
            else
            {
                ProductDTO? dto =await _applicationDbContext.Products.Select(x=>x.ConvertProductToProductDTO()).FirstOrDefaultAsync(x=>x.ProductName== name);
                return dto;
            }
        }

        //public Task<ProductDTO> SearchProduct(string searchBy, string searchText)
        //{
        //    if(string.IsNullOrEmpty(searchBy)|| string.IsNullOrEmpty(searchText))
        //    throw new NotImplementedException();
        //    else
        //    {
        //        if (searchBy == "ProductName")
        //        {

        //        }
        //    }
        //}

        public async Task<ProductUpdateDTO> UpdateAProduct(ProductUpdateDTO product)
        {
            if (product == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                ProductUpdateDTO productDTO = new ProductUpdateDTO();
                productDTO.ProductName = product.ProductName;
                productDTO.Price = product.Price;
                productDTO.ProductDescription = product.ProductDescription;
                productDTO.CategoryId = product.CategoryId;
                _applicationDbContext.Products.Update(productDTO.ConvertProductToProductUpdateDTO());
                await _applicationDbContext.SaveChangesAsync();
                return productDTO;
            }
        }
    }
}
