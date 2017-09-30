using System.ComponentModel.DataAnnotations;

namespace FirstChoiceApp.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Measurement Name is Required")]
        [Display(Name = "Measurement")]
        [StringLength(15, ErrorMessage = "Measurement should be 3 to 15 characters long", MinimumLength = 3)]
        public string MeasurementName { get; set; }
    }
}