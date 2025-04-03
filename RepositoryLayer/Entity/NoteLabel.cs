using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using RepositoryLayer.Migrations;

namespace RepositoryLayer.Entity
{
    public class NoteLabel
    {
        
        public int NoteId { get; set; }

        
        public int LabelId { get; set; }

        
        public virtual NoteEntity Note { get; set; }

        
        public virtual Label Label { get; set; }

    }
}
