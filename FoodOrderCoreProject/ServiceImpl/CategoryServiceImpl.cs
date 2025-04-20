using FoodOrderCoreProject.Domain.Entities;
using FoodOrderCoreProject.Domain.RepositoryInterfaces;
using FoodOrderCoreProject.DTOs;
using FoodOrderCoreProject.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderCoreProject.ServiceImpl
{
    public class CategoryServiceImpl:ICategoryService
    {
        private readonly ICategoryRepository _categoryrepository;
        public CategoryServiceImpl(ICategoryRepository categoryrepository)
        {
            _categoryrepository = categoryrepository;
        }
        public async Task<CategoryDTO> AddNewCategory(CategoryDTO category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            else
            {
                await _categoryrepository.AddNewCategory(category.ConvertCategoryDTOToCategory());
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
                status = await _categoryrepository.DeleteCategory(categoryName);
            }
            return status;
        }
        public async Task<List<CategoryDTO>> GetAllCategory()
        {
            var listOfCategory= await _categoryrepository.GetAllCategory();
            var listOfCategoryDTOs = listOfCategory.Select(x => x.ConvertCategoryToCategoryDTO()).ToList();
            return listOfCategoryDTOs;
        }

        public async Task<CategoryDTO> GetCategoryByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            else
            {
               var category = await _categoryrepository.GetCategoryByName(name);
                return category.ConvertCategoryToCategoryDTO();
            }
        }

        public async Task<CategoryUpdateDTO> UpdateACategory(CategoryUpdateDTO category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            else
            {
                var catUpdate = await _categoryrepository.UpdateACategory(category.ConvertCategoryUpdateDTOToCategory());
                return catUpdate.ConvertCategoryToCategoryUpdateDTO();
            }
        }
    }
}
