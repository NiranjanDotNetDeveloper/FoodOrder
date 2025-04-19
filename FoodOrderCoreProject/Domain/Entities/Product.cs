using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodOrderCoreProject.DTOs;
namespace FoodOrderCoreProject.Domain.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public double? Price { get; set; }
        public string? ProductDescription { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
    public static class ProductExetnsion
    {
        public static Product ConvertProductDTOToProduct(this ProductDTO product)
        {
            Product productEntity = new Product();
            productEntity.ProductName = product.ProductName;
            productEntity.ProductDescription = product.ProductDescription;
            productEntity.Price = product.Price;
            productEntity.CategoryId = product.CategoryId;
            productEntity.Category = product.Category.ConvertCategoryDTOToCategory();
            return productEntity;
        }
    }
}
