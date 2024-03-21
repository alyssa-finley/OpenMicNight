using OpenMicNight.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMicNight.Data
{
    public interface IPerformanceRepository
    {
        void AddPerformance(Performance performance);
        void RemovePerformance(Performance performance);
        void UpdatePerformance(Performance performance);

        Performance GetPerformanceById(int id);
        List<Performance> GetAllPerformances();
        List<Performance> GetPerformancesByName(string name);
        List<Performance> GetPerformancesByType(string type);
        void SeedPerformances();

    }
}
