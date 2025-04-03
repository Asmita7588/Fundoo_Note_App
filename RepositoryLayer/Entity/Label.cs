using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using RepositoryLayer.Migrations;

namespace RepositoryLayer.Entity
{
    
    public class Label{

        [Key]
        public int LabelId { get; set; }
        public string LabelName { get; set; }
        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual UserEntity User { get; set; }

        public ICollection<NoteLabel> NoteLabels { get; set; } = new List<NoteLabel>();
    }


}




