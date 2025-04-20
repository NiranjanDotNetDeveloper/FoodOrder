using AutoFixture;
using FluentAssertions;
using FoodOrderCoreProject.Domain.Entities;
using FoodOrderCoreProject.Domain.RepositoryInterfaces;
using FoodOrderCoreProject.DTOs;
using FoodOrderCoreProject.ServiceImpl;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderTests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly ProductServiceImpl _prodductServiceImpl;
        private readonly IFixture _fixture;
        public ProductServiceTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _prodductServiceImpl = new ProductServiceImpl(_mockProductRepository.Object);
            _fixture=new Fixture();
        }
        [Fact]
        public async Task AddProduct_WithValidData()
        {
            var product = _fixture.Create<ProductDTO>();
            _mockProductRepository.Setup(x=>x.AddNewProduct(product.ConvertProductDTOToProduct())).ReturnsAsync(product.ConvertProductDTOToProduct());
            var result=await _prodductServiceImpl.AddNewProduct(product);
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task AddProduct_WithNullValues()
        {
            _mockProductRepository.Setup(x => x.AddNewProduct(null)).ThrowsAsync(new ArgumentNullException());

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _prodductServiceImpl.AddNewProduct(null));
        }
        [Fact]
        public async Task GetAllProducts()
        {
            var product1 = _fixture.Create<ProductDTO>();
            var product2 = _fixture.Create<ProductDTO>();
            List<ProductDTO> listOfProducts = new List<ProductDTO>()
            {
                product1,product2
            };
            _mockProductRepository.Setup(x => x.GetAllProducts()).ReturnsAsync(listOfProducts.Select(x=>x.ConvertProductDTOToProduct()).ToList());
            var result = await _prodductServiceImpl.GetAllProducts();
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }
        [Fact]
        public async Task UpdateProduct_WithValidData()
        {
            var product = _fixture.Create<ProductUpdateDTO>();
            _mockProductRepository.Setup(x => x.UpdateAProduct(product.ConvertProductToProductUpdateDTO())).ReturnsAsync(product.ConvertProductToProductUpdateDTO());
            var result = await _prodductServiceImpl.UpdateAProduct(product);
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task UpdateProduct_WithNullValues()
        {
            _mockProductRepository.Setup(x => x.UpdateAProduct(null)).ThrowsAsync(new ArgumentNullException());

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _prodductServiceImpl.UpdateAProduct(null));
        }
        [Fact]
        public async Task DeleteProduct_WithValidData()
        {
            var product = _fixture.Create<ProductDTO>();
            _mockProductRepository.Setup(x => x.DeleteProduct(product.ProductName)).ReturnsAsync(true);
            var result = await _prodductServiceImpl.DeleteProduct(product.ProductName);
            result.Should().BeTrue();
        }
        [Fact]
        public async Task DeleteProduct_WithNullValues()
        {
            _mockProductRepository.Setup(x => x.DeleteProduct(null)).ThrowsAsync(new ArgumentNullException());

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _prodductServiceImpl.DeleteProduct(null));
        }
        [Fact]
        public async Task GetProductByName_WithValidData()
        {
            var product = _fixture.Create<ProductDTO>();
            _mockProductRepository.Setup(x => x.GetProductByName(product.ProductName)).ReturnsAsync(product.ConvertProductDTOToProduct());
            var result = await _prodductServiceImpl.GetProductByName(product.ProductName);
            result.Should().NotBeNull();
            result.ProductName.Should().Be(product.ProductName);
        }
        [Fact]
        public async Task GetProductByName_WithNullValues()
        {
            _mockProductRepository.Setup(x => x.AddNewProduct(null)).ThrowsAsync(new ArgumentNullException());

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
            _prodductServiceImpl.GetProductByName(null));
        }
    }
}
