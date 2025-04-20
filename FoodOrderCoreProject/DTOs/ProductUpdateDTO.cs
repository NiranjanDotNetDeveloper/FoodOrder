using FoodOrderCoreProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderCoreProject.DTOs
{
    public class ProductUpdateDTO
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public double? Price { get; set; }
        public string? ProductDescription { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public CategoryUpdateDTO? Category { get; set; }
    }
    public static class ProductUpdateDTOExtension
    {
            public static ProductUpdateDTO ConvertProductToProductUpdateDTO(this Product product)
            {
                ProductUpdateDTO productDTO = new ProductUpdateDTO();
                productDTO.ProductName = product.ProductName;
                productDTO.ProductDescription = product.ProductDescription;
                productDTO.Price = product.Price;
                productDTO.CategoryId = product.CategoryId;
                productDTO.Category = product.Category.ConvertCategoryToCategoryUpdateDTO();
                return productDTO;
            }
    }
}
