using FoodOrderCoreProject.Domain.Entities;
using FoodOrderCoreProject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderCoreProject.Domain.RepositoryInterfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategory();
        Task<Category> GetCategoryByName(string name);
        Task<Category> AddNewCategory(Category product);
        Task<Category> UpdateACategory(Category product);
        Task<bool> DeleteCategory(string categoryName);
    }
}
