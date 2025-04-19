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
        Task<List<CategoryDTO>> GetAllCategory();
        Task<CategoryDTO> GetCategoryByName(string name);
        Task<CategoryDTO> AddNewCategory(CategoryDTO product);
        Task<CategoryUpdateDTO> UpdateACategory(CategoryUpdateDTO product);
        Task<bool> DeleteCategory(string categoryName);
    }
}
