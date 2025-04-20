using AutoFixture;
using FluentAssertions;
using FoodInfrastructure.DbContextClass;
using FoodInfrastructure.RepositoryImpl;
using FoodOrderCoreProject.Domain.Entities;
using FoodOrderCoreProject.Domain.RepositoryInterfaces;
using FoodOrderCoreProject.DTOs;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderTests
{
    public class CategoryRepositoryTests:IDisposable
    {
        private readonly IFixture _fixture;
        private readonly ApplicationDbContext _dbContext;
        private readonly CategoryRepositoryImpl _categoryRepository;
        public CategoryRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().
                UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _dbContext = new ApplicationDbContext(options);
            _dbContext.Database.EnsureCreated();
            _categoryRepository = new CategoryRepositoryImpl(_dbContext);
            _fixture =new Fixture();
        }
        [Fact]
        public async Task GetAllCategory()
        {
            _dbContext.Categories.AddRange(
                   new Category() { CategoryName = "Lunch" },
                 new Category() { CategoryName = "Breakfast" },
                  new Category() { CategoryName = "Dinner" },
                   new Category() { CategoryName = "Nashta" }
                );
            _dbContext.SaveChanges();
            var result =await _categoryRepository.GetAllCategory();
            result.Should().HaveCount(5);
        }
        [Fact]
        public async Task GetCategoryByName_invalidData()
        {

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _categoryRepository.GetCategoryByName(null);
            });
        }
        [Fact]
        public async Task GetCategoryByName_ValidData()
        {
            _dbContext.Categories.Add(new Category() { CategoryName = "Lunch" } );
            _dbContext.SaveChanges();
            var result = await _categoryRepository.GetCategoryByName("Lunch");
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task AddCategoryByName_invalidData()
        {
          
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _categoryRepository.AddNewCategory(null);
            });
        }
        [Fact]
        public async Task AddCategoryByName_ValidData()
        {
            var cat = _fixture.Create<Category>();
            var result = await _categoryRepository.AddNewCategory(cat);
            result.Should().BeSameAs(cat);
            result.Should().NotBeNull();
            var inDb = _dbContext.Categories.FirstOrDefault(x => x.CategoryName == cat.CategoryName);
            inDb.Should().NotBeNull();
        }
        [Fact]
        public async Task RemoveCategoryByName_invalidData()
        {
           
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _categoryRepository.DeleteCategory(null);
            });
        }
        [Fact]
        public async Task RemoveCategoryByName_ValidData()
        {
            _dbContext.Categories.Add(new Category() { CategoryName = "Lunch" });
            _dbContext.SaveChanges();
            var result = await _categoryRepository.DeleteCategory("Lunch");
            result.Should().BeTrue();
        }
        [Fact]
        public async Task UpdateCategoryByName_invalidData()
        {
         
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await _categoryRepository.UpdateACategory(null);
            });
        }
        [Fact]
        public async Task UpdateCategoryByName_ValidData()
        {
            _dbContext.Categories.Add(new Category() { CategoryName = "Lunch" });
            _dbContext.SaveChanges();
            var cat = _dbContext.Categories.FirstOrDefault(x => x.CategoryName == "Lunch");
            cat.CategoryName = "Niranjan";
            var result = await _categoryRepository.UpdateACategory(cat);
            result.CategoryName.Should().Be("Niranjan");
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
