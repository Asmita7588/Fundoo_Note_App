using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class NotesModel
    {
        [Required(ErrorMessage = "Note name required")]
        [MinLength(3, ErrorMessage = "Note name must be at least 3 characters long")]
        [StringLength(50, ErrorMessage = "Length should not exceed 50 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Discription name required")]
        [MinLength(3, ErrorMessage = "Discription must be at least 3 characters long")]
        [StringLength(50, ErrorMessage = "Length should not exceed 50 characters")]
        public string Description { get; set; }
        
    }
}
