using System.ComponentModel.DataAnnotations.Schema;

namespace EternityX.Models
{
    public class FavoriteProduct
    {
        public int Id { get; set; }


        public string ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }



        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product{ get; set; }
    }
}
