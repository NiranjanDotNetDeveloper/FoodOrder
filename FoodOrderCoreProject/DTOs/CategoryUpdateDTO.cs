using FoodOrderCoreProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderCoreProject.DTOs
{
    public  class CategoryUpdateDTO
    {
        [Key]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
    public static class CategoryUpdateDTOExtension
    {
        public static CategoryUpdateDTO ConvertCategoryToCategoryUpdateDTO(this Category category)
        {
            CategoryUpdateDTO dto = new CategoryUpdateDTO();
            dto.CategoryId = category.CategoryId;
            dto.CategoryName = category.CategoryName;
            return dto;
        }
    }
}
