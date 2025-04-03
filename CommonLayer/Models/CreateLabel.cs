using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class CreateLabel
    {
        [Required(ErrorMessage = "Label name required")]
        [MinLength(3, ErrorMessage = "labelName must be at least 3 characters long")]
        [StringLength(50, ErrorMessage = "Length should not exceed 50 characters")]
        public string LabelName { get; set; } = string.Empty;
    }
}
