using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonRegistry.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required", AllowEmptyStrings = false)]
        [MaxLength(80, ErrorMessage = "Maximum of 80 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        [MaxLength(80, ErrorMessage = "Maximum of 80 characters")]
        public string Surname { get; set; }

        [Display(Name = "Age")]
        [Required]
        [Range(0, 200, ErrorMessage = "Age must be a positive number and less than 200")]
        public int Age { get; set; }

        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)] 
        [DisplayFormat(DataFormatString = "s")]
        public DateTime CreationDate { get; set; }
    }
}
