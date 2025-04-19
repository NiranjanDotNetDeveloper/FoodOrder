using FoodOrderCoreProject.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderCoreProject.Domain.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
    public static class CategoryExtension
    {
        public static Category ConvertCategoryDTOToCategory(this CategoryDTO category)
        {
            Category catEntity = new Category();
            catEntity.CategoryName = category.CategoryName;
            return catEntity;
        }
        public static Category ConvertCategoryUpdateDTOToCategory(this CategoryUpdateDTO category)
        {
            Category categoryEntity = new Category();
            categoryEntity.CategoryId = category.CategoryId;
            categoryEntity.CategoryName = category.CategoryName;
            return categoryEntity;
        }
    }
}
