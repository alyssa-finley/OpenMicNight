using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore; // Make sure to import EntityFrameworkCore for Include method

namespace OpenMicNight.Domain
{
    public class SignUpList
    {
        public List<Performer> Performances { get; set; }
        public SignUpList()
        {
            Performances = new List<Performer>();
        }
    }
}

//Do I need to put music and songs here?

//public class SignUpList
//{
//    public string DateTime { get; set; }
//    [ForeignKey(nameof(PerformerName))]
//    public string PerformerName {  get; set; }   
//    [ForeignKey(nameof(PerformerType))]
//    public string PerformerType { get; set; }
//    public string SongName {  get; set; }
//    public bool IsOriginal { get; set; }   

//}