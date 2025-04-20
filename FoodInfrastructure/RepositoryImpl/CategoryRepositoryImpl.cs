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
    public class CategoryRepositoryImpl : ICategoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CategoryRepositoryImpl(ApplicationDbContext dbContext)
        {
            _applicationDbContext = dbContext;
        }
        public async Task<Category> AddNewCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            else
            {
                await _applicationDbContext.Categories.AddAsync(category);
                await _applicationDbContext.SaveChangesAsync();
                return category;
            }
           
        }

        public async Task<bool> DeleteCategory(string categoryName)
        {
            bool status = false;
            if (string.IsNullOrEmpty(categoryName))
            {
                throw new ArgumentNullException(nameof(categoryName));
            }
            else
            {
                Category? cat = await _applicationDbContext.Categories.FirstOrDefaultAsync(x => x.CategoryName == categoryName);
                _applicationDbContext.Categories.Remove(cat);
                await _applicationDbContext.SaveChangesAsync();
                status= true;
            }
            return status;
        }
        public async Task<List<Category>> GetAllCategory()
        {
            return await _applicationDbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            else
            {
                Category? cat = await _applicationDbContext.Categories.FirstOrDefaultAsync(x => x.CategoryName == name);
                return cat;
            }
        }

        public async Task<Category> UpdateACategory(Category category)
        {
            if (category==null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            else
            {
                Category? catUpdate = await _applicationDbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == category.CategoryId);
                catUpdate.CategoryName = category.CategoryName;
                _applicationDbContext.Categories.Update(catUpdate);
                await _applicationDbContext.SaveChangesAsync();
                return catUpdate;
            }
        }
    }
}
