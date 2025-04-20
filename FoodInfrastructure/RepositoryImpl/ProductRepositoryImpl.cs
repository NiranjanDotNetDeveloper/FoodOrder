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
        public async Task<Product> AddNewProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            else
            {
                Product product1 = new Product();
                product1.ProductName = product.ProductName;
                product1.Price = product.Price;
                product1.ProductDescription = product.ProductDescription;
                product1.CategoryId = product.CategoryId;
                await _applicationDbContext.Products.AddAsync(product1);
                await _applicationDbContext.SaveChangesAsync();
                return product1;
            }
            
        }

        public async Task<bool> DeleteProduct(string productName)
        {
            bool isProductDeleted = false;
            Product? productToBeDeleted = await _applicationDbContext.Products.FirstOrDefaultAsync(x => x.ProductName == productName);
            if (productToBeDeleted == null)
            {
                throw new ArgumentNullException(nameof(productName));
            }
            else
            {
                _applicationDbContext.Products.Remove(productToBeDeleted);
                await _applicationDbContext.SaveChangesAsync();
                isProductDeleted = true;
            }
            return isProductDeleted;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> listOfDTO= await _applicationDbContext.Products.ToListAsync();
            if (listOfDTO.Count <= 0) {
                throw new ArgumentNullException("Products not found");
            }
            else
            {
                return listOfDTO;
            }
        }

        public async Task<Product> GetProductByName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Product Name is null or empty.");
            }
            else
            {
                Product? product =await _applicationDbContext.Products.FirstOrDefaultAsync(x=>x.ProductName== name);
                return product;
            }
        }

        public async Task<Product> UpdateAProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            else
            {
                Product? productUpdate = await _applicationDbContext.Products.FindAsync(product.ProductId); 
                productUpdate.ProductName = product.ProductName;
                productUpdate.Price = product.Price;
                productUpdate.ProductDescription = product.ProductDescription;
                productUpdate.CategoryId = product.CategoryId;
                _applicationDbContext.Products.Update(productUpdate);
                await _applicationDbContext.SaveChangesAsync();
                return productUpdate;
            }
        }
    }
}
