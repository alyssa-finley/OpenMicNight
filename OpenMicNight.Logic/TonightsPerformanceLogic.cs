using OpenMicNight.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenMicNight.Validators;
using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;
using OpenMicNight.Domain;

namespace OpenMicNight.Logic
{
    public class TonightsPerformanceLogic : IPerformanceLogic
    {
        private readonly IPerformanceRepository _performanceRepository;
        private readonly IMusicRepository _musicRepository;
        public SignUpList _signUpList;

        public TonightsPerformanceLogic(IPerformanceRepository performanceRepository)
        {
            _performanceRepository = performanceRepository;
            _performanceRepository.SeedPerformances();
            _signUpList = new SignUpList();
        }

        public void AddPerformance(Performance performance)
        {
            var validator = new PerformanceValidator();
            if (validator.Validate(performance).IsValid)
            {
                _performanceRepository.AddPerformance(performance);
            }
            else
            {
                throw new ValidationException("The type is not a valid performance type");
            }
        }
        public void RemovePerformance(Performance performance)
        {
            _performanceRepository.RemovePerformance(performance);
        }
        public void UpdatePerformance(Performance performance)
        {
            _performanceRepository.UpdatePerformance(performance);
        }
        public List<Performance> GetAllPerformances()
        {
            return _performanceRepository.GetAllPerformances();
        }
        public Performance GetPerformanceById(int id)
        {
            return _performanceRepository.GetPerformanceById(id);
        }

        public void AddPerformanceToSignUpList(Performance performance)
        {
            int index = GetIndexToInsertByPerformanceType(performance);
            _signUpList.Performances.Insert(index, performance);
        }

        public void AddPerformanceToSignUpListById(int id)
        {
            AddPerformanceToSignUpList(GetPerformanceById(id));
        }

        public bool RemovePerformanceFromSignUpListById(int id)
        {
            var item = GetPerformanceById(id);
            var wasRemoved = _signUpList.Performances.Remove(item);
            return wasRemoved; 
        }
        public SignUpList GetSignUpList()
        {
            return _signUpList;
        }
        public int GetIndexToInsertByPerformanceType(Performance performance)
        {
            if (_signUpList.Performances.Count == 0) return 0;
            List<string> type = new List<string> { "Music", "Poetry", "Comedy" };
            for (int index = 0; index <_signUpList.Performances.Count; index++)
            {
                if (type.IndexOf(performance.PerformerType) >= type.IndexOf(_signUpList.Performances[index].PerformerType)) continue;
                return index;
            }
            return _signUpList.Performances.Count;
        }
        public List<Performance> GetPerformancesByName(string name)
        {
            var namedPerformances = _performanceRepository.GetPerformancesByName(name);
            return namedPerformances;
        }

        public List<Performance> GetPerformancesByType(string type)
        {
            var typePerformances = _performanceRepository.GetPerformancesByType(type);
            return typePerformances;    
        }
    }
}
