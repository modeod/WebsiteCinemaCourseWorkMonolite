using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteCinema.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        public int MovieId { get; set; }

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }

        public int Amount { get; set; }

        public string ShoppingCarId { get; set; }
    }
}
