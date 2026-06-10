using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EternityX.Models
{
    public class ProductSize
    {
        [Key]
        public int Id { get; set; }

        // ❗ НЕ nullable, проста FK връзка
        [Required(ErrorMessage = "Моля, изберете продукт")]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }

        // Пример: 41, 42, 43, 44, 45 / S, M, L, XL / 5
        [Required(ErrorMessage = "Моля, въведете размер")]
        [MaxLength(20)]
        public string Size { get; set; }

        [Required(ErrorMessage = "Моля, въведете количество")]
        [Range(0, int.MaxValue, ErrorMessage = "Количеството не може да е отрицателно")]
        public int? Quantity { get; set; }
    }
}
