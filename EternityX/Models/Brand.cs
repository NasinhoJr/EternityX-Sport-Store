using System.ComponentModel.DataAnnotations;

namespace EternityX.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Марка")]
        public string Name { get; set; }

        [MaxLength(50)]
        [Display(Name = "Държава")]
        public string? Country { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
