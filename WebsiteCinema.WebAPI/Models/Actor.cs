using System.ComponentModel.DataAnnotations;
using WebsiteCinema.WebAPI.Base;

namespace WebsiteCinema.WebAPI.Models
{
    public class Actor : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Profile Picture")]
        public string ProfilePicturURL { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(200, ErrorMessage ="Max Lenght is 200")]
        public string FullName { get; set; }

        [Display(Name = "Biography")]
        [StringLength(200, ErrorMessage ="Max Lenght is 200")]
        public string Bio { get; set; }

        public List<Actor_Movie>? Actor_Movies { get; set; }
    }
}
