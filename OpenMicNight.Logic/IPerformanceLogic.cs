using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenMicNight.Data;
using OpenMicNight.Domain;

namespace OpenMicNight.Logic
{
    public interface IPerformanceLogic 
    {
       void AddPerformance(Performance performance);
        void RemovePerformance(Performance performance);   
        void UpdatePerformance(Performance performance);
        List<Performance> GetAllPerformances();
        Performance GetPerformanceById(int id);
        List<Performance> GetPerformancesByName(string name);
        List<Performance> GetPerformancesByType(string type);   
    }
}
