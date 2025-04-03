using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User name required")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        [StringLength(50, ErrorMessage = "Length should not exceed 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "User name required")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        [StringLength(50, ErrorMessage = "Length should not exceed 50 characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        [RegularExpression("[^0-9{4}-0-9{2}-0-9{2}]")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Must provide an email address")]
        [EmailAddress(ErrorMessage = "Must be a valid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Password length should be between 3 and 50 characters")]
        public string Password { get; set; }
    }
}
