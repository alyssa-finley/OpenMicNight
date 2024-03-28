using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenMicNight.Data;
using OpenMicNight.Domain;

namespace OpenMicNight.Logic
{
    public interface IPerformerLogic
    {
        //bool
        void AddPerformer(Performer performance);
        void RemovePerformer(Performer performance);
        void UpdatePerformer(Performer performance);
        List<Performer> GetAllPerformers();
        Performer GetPerformersById(int id);
        List<Performer> GetPerformersByName(string name);
        List<Performer> GetPerformersByType(string type);
    }
} 
