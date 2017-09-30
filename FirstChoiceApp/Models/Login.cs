using System.ComponentModel.DataAnnotations;

namespace FirstChoiceApp.Models
{
    public class Login
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression("^([a-zA-Z0-9@]+)$", ErrorMessage = "Invalid Password Type")]
        public string Password { get; set; }
    }
}