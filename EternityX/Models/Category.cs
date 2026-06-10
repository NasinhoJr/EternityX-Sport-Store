using System.ComponentModel.DataAnnotations;

namespace EternityX.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Категория")]
        public string Name { get; set; }

        [MaxLength(200)]
        [Display(Name = "Описание")]
        public string? Description { get; set; }


        public ICollection<Product>? Products { get; set; }
    }
}
