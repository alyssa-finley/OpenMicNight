using Microsoft.EntityFrameworkCore;
using OpenMicNight.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMicNight.Data
{
    public class PerformanceRepository : IPerformanceRepository
    {
        private readonly PerformanceContext _dbContext;

        public PerformanceRepository()
        {
            _dbContext = new PerformanceContext();
        }
    public void AddPerformance(Performance performance)
        {
            _dbContext.Performance.Add(performance);
            _dbContext.SaveChanges();
        }
    public void RemovePerformance(Performance performance)
        {
            _dbContext.Performance.Remove(performance);
            _dbContext.SaveChanges();
        }
    public void UpdatePerformance(Performance performance)
        {
            _dbContext.Performance.Update(performance);
            _dbContext.SaveChanges();
        }
    public List<Performance> GetAllPerformances()
        {
            return _dbContext.Performance.ToList();
        }
    public Performance GetPerformanceById(int id)
        {
            return _dbContext.Performance.SingleOrDefault(x => x.PerformerId == id);
        }
    public List<Performance> GetPerformancesByName(string name)
        {
            return _dbContext.Performance
                .FromSql($"SELECT * FROM [Performance] WHERE PerformerName = {name}")
                .ToList();
        }
    public List<Performance> GetPerformancesByType(string type)

        {
            return _dbContext.Performance
                .FromSql($"SELECT * FROM [Performance] WHERE PerformerType = {type}")
                .ToList();
        }
    public void SeedPerformances()
        {
            if(!_dbContext.Performance.Any())
            {
                _dbContext.Performance.AddRange(
                    new Performance
                    {
                        PerformerName = "Two For One",
                        PerformerType = "Music",
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    }, 
                    new Performance
                    {
                        PerformerName = "Mama Said",
                        PerformerType = "Music", 
                        Datetime= DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    }, 
                    new Performance
                    {
                        PerformerName = "Jay T", 
                        PerformerType = "Poetry", 
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    }, 
                    new Performance
                    {
                        PerformerName = "Jaclyn H.", 
                        PerformerType = "Poetry", 
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    },
                    new Performance
                    {
                        PerformerName = "Gary W",
                        PerformerType = "Music",
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    },
                    new Performance
                    {
                        PerformerName = "Ezbzy",
                        PerformerType = "Comedy", 
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    }, 
                    new Performance
                    {
                        PerformerName = "TK Trio", 
                        PerformerType = "Music", 
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    }, 
                    new Performance
                    {
                        PerformerName = "Adam R.", 
                        PerformerType = "Comedy", 
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    }, 
                    new Performance
                    {
                        PerformerName = "Richard", 
                        PerformerType = "Music", 
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    }, 
                    new Performance
                    {
                        PerformerName = "The Lost Pockets", 
                        PerformerType = "Music", 
                        Datetime= DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    }, 
                    new Performance
                    {
                        PerformerName = "Tucker D", 
                        PerformerType = "Music", 
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    }, 
                    new Performance
                    {
                        PerformerName = "Alan", 
                        PerformerType = "Comedy", 
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    }, 
                    new Performance
                    {
                        PerformerName = "Jondra Nic", 
                        PerformerType = "Music", 
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    }
                    );
                _dbContext.SaveChanges();
            }
        }
            
    }
}
