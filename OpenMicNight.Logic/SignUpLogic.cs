using OpenMicNight.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;
using OpenMicNight.Domain;
using Microsoft.EntityFrameworkCore;

namespace OpenMicNight.Logic
{
    public class SignUpLogic : ISignUpLogic
    {
        private readonly IPerformerRepository _performanceRepository;
        private readonly ISongRepository _songRepository;   
        public SignUpList _signUpList;

        public SignUpLogic(IPerformerRepository performanceRepository)
        {
            _performanceRepository = performanceRepository;
            _performanceRepository.SeedPerformers();
            _signUpList = new SignUpList();
        }
        public void AddPerformer(Performer performance)
        {
               _performanceRepository.AddPerformer(performance);
        }
        public void RemovePerformer(Performer performance)
        {
            _performanceRepository.RemovePerformer(performance);
        }
        public void UpdatePerformer(Performer performance)
        {
            _performanceRepository.UpdatePerformer(performance);
        }
        public bool MaxPerformances()
        {
            return _signUpList.Performances.Count < 13;
        }
        public List<Performer> GetAllPerformers()
        {
            return _performanceRepository.GetAllPerformers();
        }
        public Performer GetPerformersById(int id)
        {
            return _performanceRepository.GetPerformersById(id);
        }

        public void AddPerformanceToSignUpList(Performer performance)
        {
            int index = GetIndexToInsertByPerformanceType(performance);
            _signUpList.Performances.Insert(index, performance);
        }

        public void AddPerformanceToSignUpListById(int id)
        {
            AddPerformanceToSignUpList(GetPerformersById(id));
        }

        public bool RemovePerformanceFromSignUpListById(int id)
        {
            var item = GetPerformersById(id);
            var wasRemoved = _signUpList.Performances.Remove(item);
            return wasRemoved;
        }
        public SignUpList GetSignUpList()
        {
            return _signUpList;
        }
        public int GetIndexToInsertByPerformanceType(Performer performance)
        {
            if (_signUpList.Performances.Count == 0) return 0;
            List<string> type = new List<string> { "Music", "Poetry", "Comedy" };
            for (int index = 0; index < _signUpList.Performances.Count; index++)
            {
                if (type.IndexOf(performance.PerformerType) >= type.IndexOf(_signUpList.Performances[index].PerformerType)) continue;
                return index;
            }
            return _signUpList.Performances.Count;
        }
        public List<Performer> GetPerformersByName(string name)
        {
            var namedPerformers = _performanceRepository.GetPerformersByName(name);
            return namedPerformers;
        }

        public List<Performer> GetPerformersByType(string type)
        {
            var typePerformances = _performanceRepository.GetPerformersByType(type);
            return typePerformances;
        }

        public void AddSong(Song song)
        {
            _songRepository.AddSong(song);
        }

        public void RemoveSong(Song song)
        {
            _songRepository.RemoveSong(song);
        }

        public void UpdateSong(Song song)
        {
            _songRepository.UpdateSong(song);
        }

        public List<Song> GetAllSongs()
        {
            return _songRepository.GetAllSongs();
        }
    }
}

