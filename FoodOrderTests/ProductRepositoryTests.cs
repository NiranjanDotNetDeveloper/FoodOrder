using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using FoodInfrastructure.DbContextClass;
using FoodInfrastructure.RepositoryImpl;
using FoodOrderCoreProject.Domain.Entities;
using FoodOrderCoreProject.Domain.RepositoryInterfaces;
using FoodOrderCoreProject.DTOs;
using Microsoft.EntityFrameworkCore;
using Moq;
namespace FoodOrderTests
{

    public class ProductRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductRepositoryImpl _productRepository;
        private readonly IFixture _fixture;

        public ProductRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unique db for isolation
                .Options;

            _context = new ApplicationDbContext(options);
            _context.Database.EnsureCreated();
            _productRepository = new ProductRepositoryImpl(_context);
            _fixture = new Fixture();
        }
        [Fact]
        public async Task AddProduct_WithValidData()
        {
            var product = new Product
            {
                ProductName = "Pizza",
                Price = 150,
                ProductDescription = "Cheesy pizza",
                CategoryId = 1
            };
            var result = await _productRepository.AddNewProduct(product);
            result.Should().NotBeNull();
            var inDb = await _context.Products.FirstOrDefaultAsync(product =>product.ProductName== result.ProductName);
            inDb.Should().NotBeNull();
        }
        [Fact]
        public async Task AddProduct_WithNullValues()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _productRepository.AddNewProduct(null));
        }
        [Fact]
        public async Task GetAllProduct()
        {
            _context.Products.AddRange(
     new Product { ProductName = "A", Price = 50, ProductDescription = "A desc", CategoryId = 1 },
     new Product { ProductName = "B", Price = 70, ProductDescription = "B desc", CategoryId = 1 }
 );
            await _context.SaveChangesAsync();

            List<Product> listOfProductCount = await _productRepository.GetAllProducts();
            listOfProductCount.Should().HaveCount(3);
        }
        [Fact]
        public async Task RemoveProduct_InvalidData()
        {

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _productRepository.DeleteProduct(null);
            });
        }
        [Fact]
        public async Task RemoveProduct_DeleteProductValidData()
        {
            _context.Products.Add(new Product { ProductName = "A", Price = 50, ProductDescription = "A desc", CategoryId = 1 });
            _context.SaveChanges();
            bool result = await _productRepository.DeleteProduct("A");
            result.Should().BeTrue();
        }
        [Fact]
        public async Task GetProductByName_InvalidParam()
        {

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _productRepository.GetProductByName(null);
            });
        }
        [Fact]
        public async Task GetProductByName_ValidParam()
        {
            _context.Products.Add(new Product { ProductName = "A", Price = 50, ProductDescription = "A desc", CategoryId = 1 });
            _context.SaveChanges();
            var result = await _productRepository.GetProductByName("A");
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task UpdateProduct_WithValidData()
        {

            _context.Products.Add(new Product { ProductName = "A", Price = 50, ProductDescription = "A desc", CategoryId = 1 });
            _context.SaveChanges();
            var product = _context.Products.FirstOrDefault(x => x.ProductName == "A");
            product.ProductDescription = "Niranjan Hi";
            var result2 = await _productRepository.UpdateAProduct(product);
            result2.ProductDescription.Should().Be("Niranjan Hi");
        }
        [Fact]
        public async Task UpdateProduct_WithNullValues()
        {


            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _productRepository.UpdateAProduct(null);
            });
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
