using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebsiteCinema.Data;
using WebsiteCinema.Data.Base;

namespace WebsiteCinema.Models
{
    public class VMNewMovie
    {
        public VMNewMovie()
        {
            ActorIds = new List<int>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Price in $")]
        public double Price { get; set; }

        [Display(Name = "Poster")]
        public string ImageURL { get; set; }

        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public MovieCategoryEnum MovieCategory { get; set; }

        public int CinemaId { get; set; }
        public int ProducerId { get; set; }
        public List<int> ActorIds { get; set; }
    }
}
