using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace EternityX.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        [Display(Name = "Име на продукта")]
        public string Name { get; set; }

        [MaxLength(500)]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Required]
        [DisplayName("Цена (€)")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Display(Name = "Наличност (общо)")]
        [Range(0, int.MaxValue, ErrorMessage = "Наличността не може да бъде отрицателна.")]
        public int Stock { get; set; }

        [MaxLength(20)]
        [Display(Name = "Цвят")]
        public string? Color { get; set; }

        public ICollection<ProductSize>? Sizes { get; set; }

        // Категория
        [Required]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        // Марка
        [Required]
        [Display(Name = "Марка")]
        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public Brand? Brand { get; set; }

        // Пази се в базата (име на файл ИЛИ URL)
        [MaxLength(300)]
        [DisplayName("Снимка")]
        public string? Picture { get; set; }

        // ========== САМО ЗА FORM ==========
        [NotMapped]
        [DisplayName("Качи снимка (файл)")]
        public IFormFile? PictureFile { get; set; }

        [NotMapped]
        [DisplayName("Снимка (URL)")]
        [Url]
        public string? PictureUrl { get; set; }

        public ICollection<FavoriteProduct>? FavoriteProducts { get; set; }
    }
}
