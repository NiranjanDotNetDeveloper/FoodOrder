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
        public async Task<CategoryDTO> AddNewCategory(CategoryDTO category)
        {
            if (category == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                await _applicationDbContext.Categories.AddAsync(category.ConvertCategoryDTOToCategory());
                await _applicationDbContext.SaveChangesAsync();
                return category;
            }
           
        }

        public async Task<bool> DeleteCategory(string categoryName)
        {
            bool status = false;
            if (string.IsNullOrEmpty(categoryName))
            {
                throw new NotImplementedException();
            }
            else
            {
                CategoryDTO? cat = await _applicationDbContext.Categories.Select(x => x.ConvertCategoryToCategoryDTO()).FirstOrDefaultAsync(x => x.CategoryName == categoryName);
                _applicationDbContext.Categories.Remove(cat.ConvertCategoryDTOToCategory());
                await _applicationDbContext.SaveChangesAsync();
                status= true;
            }
            return status;
        }
        public async Task<List<CategoryDTO>> GetAllCategory()
        {
            return await _applicationDbContext.Categories.Select(x => x.ConvertCategoryToCategoryDTO()).ToListAsync();
        }

        public async Task<CategoryDTO> GetCategoryByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new NotImplementedException();
            }
            else
            {
                CategoryDTO? cat = await _applicationDbContext.Categories.Select(x => x.ConvertCategoryToCategoryDTO()).FirstOrDefaultAsync(x => x.CategoryName == name);
                return cat;
            }
        }

        public async Task<CategoryUpdateDTO> UpdateACategory(CategoryUpdateDTO category)
        {
            if (category==null)
            {
                throw new NotImplementedException();
            }
            else
            {
                CategoryUpdateDTO? catUpdate = await _applicationDbContext.Categories.Select(x => x.ConvertCategoryToCategoryUpdateDTO()).FirstOrDefaultAsync(x => x.CategoryName == category.CategoryName);
                catUpdate.CategoryName = category.CategoryName;
                _applicationDbContext.Categories.Update(catUpdate.ConvertCategoryUpdateDTOToCategory());
                await _applicationDbContext.SaveChangesAsync();
                return catUpdate;
            }
        }
    }
}
