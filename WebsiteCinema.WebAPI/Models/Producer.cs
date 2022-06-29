using System.ComponentModel.DataAnnotations;
using WebsiteCinema.WebAPI.Base;

namespace WebsiteCinema.WebAPI.Models
{
    public class Producer : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage ="Picture is Required")]
        public string ProfilePicturURL { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is Required")]
        public string FullName { get; set; }

        [Display(Name = "Biography")]
        [StringLength(200, ErrorMessage ="Max Lenght is 200")]
        public string Bio { get; set; }

        public List<Movie>? Movies { get; set; }
    }
}
