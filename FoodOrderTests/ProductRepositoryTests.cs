using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using FoodOrderCoreProject.Domain.RepositoryInterfaces;
using FoodOrderCoreProject.DTOs;
using Moq;
namespace FoodOrderTests
{
 
    public class ProductRepositoryTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IProductRepository> _mockProductRepo;
        public ProductRepositoryTests()
        {
            _mockProductRepo = new Mock<IProductRepository>();
            _fixture= new Fixture();
        }
        [Fact]
        public async Task AddProduct_WithValidData()
        {
            ProductDTO dto1=_fixture.Create<ProductDTO>();
             _mockProductRepo
           .Setup(repo => repo.AddNewProduct(It.IsAny<ProductDTO>()))
           .ReturnsAsync(dto1);
            var result = await _mockProductRepo.Object.AddNewProduct(dto1);
            result.Should().BeSameAs(dto1);
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task AddProduct_WithNullValues()
        {
           
            _mockProductRepo.Setup(x => x.AddNewProduct(null)).ThrowsAsync(new NotImplementedException());
            await Assert.ThrowsAsync<NotImplementedException>(async () =>
            {
                await _mockProductRepo.Object.AddNewProduct(null);
            });
        }
        [Fact]
        public async Task GetAllProduct()
        {
            ProductDTO dt1 = _fixture.Create<ProductDTO>();
            ProductDTO dt2 = _fixture.Create<ProductDTO>();
            ProductDTO dt3 = _fixture.Create<ProductDTO>();
            List<ProductDTO> listOfProduct = new List<ProductDTO>() {
                dt1,dt2,dt3
            };
            _mockProductRepo.Setup(x => x.GetAllProducts()).ReturnsAsync(listOfProduct);
            List<ProductDTO> listOfProductCount = await _mockProductRepo.Object.GetAllProducts();
            listOfProductCount.Should().HaveCount(3);
            listOfProductCount.Should().Contain(dt1);
            listOfProductCount.Should().Contain(dt2);
            listOfProductCount.Should().Contain(dt3);
        }
        [Fact]
        public async Task RemoveProduct_InvalidData()
        {
            _mockProductRepo.Setup(x => x.DeleteProduct(null)).ThrowsAsync(new NotImplementedException());
            await Assert.ThrowsAsync<NotImplementedException>(async () =>
            {
                await _mockProductRepo.Object.DeleteProduct(null);
            });
        }
        [Fact]
        public async Task RemoveProduct_DeleteProductValidData()
        {
            var product = _fixture.Create<ProductDTO>();
            var result = true;
            _mockProductRepo.Setup(x => x.DeleteProduct(product.ProductName)).ReturnsAsync(result);
            var resultGet=await _mockProductRepo.Object.DeleteProduct(product.ProductName);
            resultGet.Should().BeTrue();
        }
        [Fact]
        public async Task GetProductByName_InvalidParam()
        {
            _mockProductRepo.Setup(x => x.GetProductByName(null)).ThrowsAsync(new NotImplementedException());
            await Assert.ThrowsAsync<NotImplementedException>(async () =>
            {
                await _mockProductRepo.Object.GetProductByName(null);
            });
        }
        [Fact]
        public async Task GetProductByName_ValidParam()
        {
            var name = _fixture.Create<string>();
            ProductDTO prod = _fixture.Create<ProductDTO>();
            _mockProductRepo.Setup(x => x.GetProductByName(prod.ProductName)).ReturnsAsync(prod);
            var result = await _mockProductRepo.Object.GetProductByName(prod.ProductName);
            result.Should().BeSameAs(prod);
        }
        [Fact]
        public async Task UpdateProduct_WithValidData()
        {
          
            ProductUpdateDTO prodUpdate = _fixture.Create<ProductUpdateDTO>();
            _mockProductRepo.Setup(x => x.UpdateAProduct(It.IsAny<ProductUpdateDTO>())).ReturnsAsync(prodUpdate);
            var result2 = await _mockProductRepo.Object.UpdateAProduct(prodUpdate);
            result2.Should().BeSameAs(prodUpdate);
        }
        [Fact]
        public async Task UpdateProduct_WithNullValues()
        {

            _mockProductRepo.Setup(x => x.UpdateAProduct(null)).ThrowsAsync(new NotImplementedException());
            await Assert.ThrowsAsync<NotImplementedException>(async () =>
            {
                await _mockProductRepo.Object.UpdateAProduct(null);
            });
        }
    }
}
