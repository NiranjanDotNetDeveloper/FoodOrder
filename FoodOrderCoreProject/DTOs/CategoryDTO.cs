using FoodOrderCoreProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderCoreProject.DTOs
{
    public class CategoryDTO
    {
        public string? CategoryName { get; set; }
    }
    public static class CategoryDTOExtension
    {
        public static CategoryDTO ConvertCategoryToCategoryDTO(this Category category)
        {
            CategoryDTO dto = new CategoryDTO();
            dto.CategoryName = category.CategoryName;
            return dto;
        }
    }

}
