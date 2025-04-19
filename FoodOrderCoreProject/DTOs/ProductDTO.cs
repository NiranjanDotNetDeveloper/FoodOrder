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
    public class ProductDTO
    {
        public string? ProductName { get; set; }
        public double? Price { get; set; }
        public string? ProductDescription { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public CategoryDTO? Category { get; set; }
    }
    public static class ProductDTOExetnsion
    {
        public static ProductDTO ConvertProductToProductDTO(this Product product)
        {
            ProductDTO productDTO = new ProductDTO();
            productDTO.ProductName = product.ProductName;
            productDTO.ProductDescription = product.ProductDescription;
            productDTO.Price = product.Price;
            productDTO.CategoryId=product.CategoryId;
            productDTO.Category = product.Category.ConvertCategoryToCategoryDTO();
            return productDTO;
        }
        public static Product ConvertProductToProductUpdateDTO(this ProductUpdateDTO product)
        {
            Product productEntity = new Product();
            productEntity.ProductName = product.ProductName;
            productEntity.ProductDescription = product.ProductDescription;
            productEntity.Price = product.Price;
            productEntity.CategoryId = product.CategoryId;
            productEntity.Category = product.Category.ConvertCategoryUpdateDTOToCategory();
            return productEntity;
        }
    }
}
