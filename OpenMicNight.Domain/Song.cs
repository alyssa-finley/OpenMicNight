using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMicNight.Domain
{
    //Single Responsibility Principle (SRP) - this Song class has a single responsibility representing the songs and ensuring they are associated with a performer. 
    //This class is responsible for storing information about the song itself. 
    public class Song
    {
        [Key]
        public int SongId { get; set; } // Primary key for Song entity
        public Performer Performer { get; set; } // Performer navigation property

        [ForeignKey("Performer")]
        public int PerformerId { get; set; } // Foreign key to Performer
        public string SongName { get; set; }
        public bool IsOriginal { get; set; }
    }
}
