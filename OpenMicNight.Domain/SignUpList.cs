using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore; // Make sure to import EntityFrameworkCore for Include method

namespace OpenMicNight.Domain
{
    //Single Responsibility Principle (SRP) - this SignUpList class has a single responsibility representing the sign up list. 
    //This class manages the list of performer signed up for performances. 
    public class SignUpList
    {
        public List<Performer> Performances { get; set; }
        public SignUpList()
        {
            Performances = new List<Performer>();
        }
    }
}
