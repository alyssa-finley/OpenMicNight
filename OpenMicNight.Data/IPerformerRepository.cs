using OpenMicNight.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMicNight.Data
{
    public interface IPerformerRepository
    {
        //bool AddPerformer(Peformer performance);
        void AddPerformer(Performer performer);  
        void RemovePerformer(Performer performer);
        void UpdatePerformer(Performer performer);
        Performer GetPerformersById(int id);
        List<Performer> GetAllPerformers();
        List<Performer> GetPerformersByName(string name);
        List<Performer> GetPerformersByType(string type);
        void SeedPerformers();
    }
}
