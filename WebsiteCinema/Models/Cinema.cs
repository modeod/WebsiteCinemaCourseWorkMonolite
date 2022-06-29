using System.ComponentModel.DataAnnotations;
using WebsiteCinema.Data.Base;

namespace WebsiteCinema.Models
{
    public class Cinema : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Logo")]
        [Required(ErrorMessage ="Logo is required")]
        public string Logo { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public List<Movie>? Movies { get; set; }
    }
}
