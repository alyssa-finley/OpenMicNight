using OpenMicNight.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMicNight.Data
{
    public class SignUpList
    {
        public List<Performance> Performances {  get; set; }  
        
        //Do I need to put music and songs here?

        public SignUpList()
        {
            Performances = new List<Performance>();
        }
    }
}
