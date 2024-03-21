using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMicNight.Domain
{
    public class Song
    {
        [Key]
        public int SongId { get; set; } // Primary key for Song entity
        public int PerformerId { get; set; } // Foreign key to Performer
        public Performance Performance { get; set; } // Performer navigation property
        public int MusicId { get; set; } // Foreign key to Music
        public Music Music { get; set; } // Music navigation property
        public string SongName { get; set; }
        public bool IsOriginal { get; set; }
    }
}
