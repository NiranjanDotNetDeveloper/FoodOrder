using AutoFixture;
using FluentAssertions;
using FoodInfrastructure.DbContextClass;
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
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepository;
        private readonly CategoryServiceImpl _categoryServiceImpl;
        private readonly IFixture _fixture;
        public CategoryServiceTests()
        {
            _categoryRepository = new Mock<ICategoryRepository>();
            _categoryServiceImpl = new CategoryServiceImpl(_categoryRepository.Object);
            _fixture = new Fixture();
        }
        [Fact]
        public async Task GetAllCategory()
        {
            var cat1 = _fixture.Create<CategoryDTO>();
            var cat2 = _fixture.Create<CategoryDTO>();
            List<CategoryDTO> listOfCat = new List<CategoryDTO>()
            {
                cat1,cat2
            };
            _categoryRepository.Setup(x => x.GetAllCategory()).ReturnsAsync(listOfCat.Select(x=>x.ConvertCategoryDTOToCategory()).ToList());
            var listOfCatResults=await _categoryServiceImpl.GetAllCategory();
            listOfCatResults.Should().HaveCount(2);
            listOfCat.Should().Contain(cat1);
            listOfCat.Should().Contain(cat2);
        }
        [Fact]
        public async Task AddCategory_ValidData()
        {
            var cat1 = _fixture.Create<CategoryDTO>();
            _categoryRepository.Setup(x => x.AddNewCategory(cat1.ConvertCategoryDTOToCategory())).ReturnsAsync(cat1.ConvertCategoryDTOToCategory());
            var result = await _categoryServiceImpl.AddNewCategory(cat1);
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(cat1);
        }
        [Fact]
        public async Task AddCategory_InvalidData()
        {
          
            _categoryRepository.Setup(x => x.AddNewCategory(null)).ThrowsAsync(new ArgumentNullException());
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _categoryServiceImpl.AddNewCategory(null);
            });
        }
        [Fact]
        public async Task UpdateCategory_ValidData()
        {
            var cat1 = _fixture.Create<CategoryUpdateDTO>();
            _categoryRepository.Setup(x => x.UpdateACategory(cat1.ConvertCategoryUpdateDTOToCategory())).ReturnsAsync(cat1.ConvertCategoryUpdateDTOToCategory());
            var result = await _categoryServiceImpl.UpdateACategory(cat1);
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task UpdateCategory_InvalidData()
        {

            _categoryRepository.Setup(x => x.UpdateACategory(null)).ThrowsAsync(new ArgumentNullException());
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _categoryServiceImpl.UpdateACategory(null);
            });
        }
        [Fact]
        public async Task DeleteCategory_ValidData()
        {
            var cat1 = _fixture.Create<CategoryUpdateDTO>();
            _categoryRepository.Setup(x => x.DeleteCategory(cat1.CategoryName)).ReturnsAsync(true);
            var result = await _categoryServiceImpl.DeleteCategory(cat1.CategoryName);
            result.Should().BeTrue();
        }
        [Fact]
        public async Task DeleteCategory_InvalidData()
        {

            _categoryRepository.Setup(x => x.DeleteCategory(null)).ThrowsAsync(new ArgumentNullException());
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _categoryServiceImpl.DeleteCategory(null);
            });
        }
        [Fact]
        public async Task GetCategoryByName_ValidData()
        {
            var cat1 = _fixture.Create<CategoryDTO>();
            _categoryRepository.Setup(x => x.GetCategoryByName(cat1.CategoryName)).ReturnsAsync(cat1.ConvertCategoryDTOToCategory());
            var result = await _categoryServiceImpl.GetCategoryByName(cat1.CategoryName);
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task GetCategoryByName_InvalidData()
        {

            _categoryRepository.Setup(x => x.GetCategoryByName(null)).ThrowsAsync(new ArgumentNullException());
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _categoryServiceImpl.GetCategoryByName(null);
            });
        }
    }

}
