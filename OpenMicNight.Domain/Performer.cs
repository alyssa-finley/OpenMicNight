using System.ComponentModel.DataAnnotations;

namespace OpenMicNight.Domain
{
    //Single Responsibility Principle (SRP) - this Performer class has a single responsibility representing the performers. 
    //This class manages the performer details. 
    public class Performer
    {
        [Key]
        public int PerformerId { get; set; }
        public string PerformerName { get; set; }
        public string PerformerType { get; set; } 
        public string Datetime { get; set; }
        public List<Song> Songs { get; set; }

    }
}

