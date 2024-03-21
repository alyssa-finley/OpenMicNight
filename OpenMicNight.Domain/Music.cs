using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMicNight.Domain
{
    //a class for all musician details including a list of songs
    public class Music
    {
        [Key]
        public int MusicId { get; set; }   // Adding a separate ID for Music entity 
        public int PerformerId { get; set; } // Foreign key
        public string PerformerName { get; set; } // Additional property
        public Performance Performance { get; set; }  // Performer navigation property
        public List<Song> Songs { get; set; } = new List<Song>();
    } 
}

//Adding a one to one relationship in defining relationships in your model to make a comedy class in a one to one relationship