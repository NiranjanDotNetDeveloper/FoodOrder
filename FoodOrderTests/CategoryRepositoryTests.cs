using AutoFixture;
using FluentAssertions;
using FoodOrderCoreProject.Domain.RepositoryInterfaces;
using FoodOrderCoreProject.DTOs;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderTests
{
    public class CategoryRepositoryTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepository;
        private readonly IFixture _fixture;
        public CategoryRepositoryTests()
        {
            _categoryRepository = new Mock<ICategoryRepository>();
            _fixture=new Fixture();
        }
        [Fact]
        public async Task GetAllCategory()
        {
            List<CategoryDTO> listOfcategory = new List<CategoryDTO>()
            {
                new CategoryDTO(){CategoryName="Lunch"},
                 new CategoryDTO(){CategoryName="Breakfast"},
                  new CategoryDTO(){CategoryName="Dinner"},
                   new CategoryDTO(){CategoryName="Nashta"},
            };
            _categoryRepository.Setup(x => x.GetAllCategory()).ReturnsAsync(listOfcategory);
            var resultList = await _categoryRepository.Object.GetAllCategory();
            resultList.Should().HaveCount(4);
        }
        [Fact]
        public async Task GetCategoryByName_invalidData()
        {
            _categoryRepository.Setup(x => x.GetCategoryByName(null)).ThrowsAsync(new NotImplementedException());
            await Assert.ThrowsAsync<NotImplementedException>(async () =>
            {
                await _categoryRepository.Object.GetCategoryByName(null);
            });
        }
        [Fact]
        public async Task GetCategoryByName_ValidData()
        {
            CategoryDTO cat = _fixture.Create<CategoryDTO>();
            _categoryRepository.Setup(x => x.GetCategoryByName(cat.CategoryName)).ReturnsAsync(cat);
            var result = await _categoryRepository.Object.GetCategoryByName(cat.CategoryName);
            result.Should().BeSameAs(cat);
        }
        [Fact]
        public async Task AddCategoryByName_invalidData()
        {
            _categoryRepository.Setup(x => x.AddNewCategory(null)).ThrowsAsync(new NotImplementedException());
            await Assert.ThrowsAsync<NotImplementedException>(async () =>
            {
                await _categoryRepository.Object.AddNewCategory(null);
            });
        }
        [Fact]
        public async Task AddCategoryByName_ValidData()
        {
            CategoryDTO cat = _fixture.Create<CategoryDTO>();
            _categoryRepository.Setup(x => x.AddNewCategory(It.IsAny<CategoryDTO>())).ReturnsAsync(cat);
            var result = await _categoryRepository.Object.AddNewCategory(cat);
            result.Should().BeSameAs(cat);
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task RemoveCategoryByName_invalidData()
        {
            _categoryRepository.Setup(x => x.DeleteCategory(null)).ThrowsAsync(new NotImplementedException());
            await Assert.ThrowsAsync<NotImplementedException>(async () =>
            {
                await _categoryRepository.Object.DeleteCategory(null);
            });
        }
        [Fact]
        public async Task RemoveCategoryByName_ValidData()
        {
            CategoryDTO cat = _fixture.Create<CategoryDTO>();
            _categoryRepository.Setup(x => x.DeleteCategory(cat.CategoryName)).ReturnsAsync(true);
            var result = await _categoryRepository.Object.DeleteCategory(cat.CategoryName);
            result.Should().BeTrue();
            
        }
        [Fact]
        public async Task UpdateCategoryByName_invalidData()
        {
            _categoryRepository.Setup(x => x.UpdateACategory(null)).ThrowsAsync(new NotImplementedException());
            await Assert.ThrowsAsync<NotImplementedException>(async () =>
            {
                await _categoryRepository.Object.UpdateACategory(null);
            });
        }
        [Fact]
        public async Task UpdateCategoryByName_ValidData()
        {
            CategoryUpdateDTO cat = _fixture.Create<CategoryUpdateDTO>();
            _categoryRepository.Setup(x => x.UpdateACategory(It.IsAny<CategoryUpdateDTO>())).ReturnsAsync(cat);
            var result = await _categoryRepository.Object.UpdateACategory(cat);
            result.Should().BeSameAs(cat);
        }
    }
}
