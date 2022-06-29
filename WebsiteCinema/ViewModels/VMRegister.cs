using System.ComponentModel.DataAnnotations;

namespace WebsiteCinema.ViewModels
{
    public class VMRegister
    {
        [Required(ErrorMessage = "Username is required ")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Full name is required ")]
        public string FullName { get; set; }

        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
