using Microsoft.EntityFrameworkCore;
using OpenMicNight.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMicNight.Data
{
    public class PerformerRepository : IPerformerRepository
    {
        private readonly PerformerContext _dbContext;

        public PerformerRepository()
        {
            _dbContext = new PerformerContext();
        }
    public void AddPerformer(Performer performer)  
        {
            _dbContext.Performer.Add(performer);
            _dbContext.SaveChanges();
        } 
    public void RemovePerformer(Performer performer)
        {
            _dbContext.Performer.Remove(performer);
            _dbContext.SaveChanges();
            // var success = false; 
            // success = 
        }
        public void UpdatePerformer(Performer performer)
        {
            _dbContext.Performer.Update(performer);
            _dbContext.SaveChanges();
        }
    public List<Performer> GetAllPerformers()
        {
            return _dbContext.Performer.ToList();
        }
    public Performer GetPerformersById(int id)
        {
            return _dbContext.Performer.SingleOrDefault(x => x.PerformerId == id);
        }
    public List<Performer> GetPerformersByName(string name)
        {
            return _dbContext.Performer
                .FromSql($"SELECT * FROM [Performer] WHERE PerformerName = {name}")
                .ToList();
        }
    public List<Performer> GetPerformersByType(string type)

        {
            return _dbContext.Performer
                .FromSql($"SELECT * FROM [Performer] WHERE PerformerType = {type}")
                .ToList();
        }
    public void SeedPerformers()
        {
            if(!_dbContext.Performer.Any())
            {
                _dbContext.Performer.AddRange(
                    new Performer
                    {
                        PerformerName = "Two For One",
                        PerformerType = "Music",
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    }, 
                    new Performer
                    {
                        PerformerName = "Mama Said",
                        PerformerType = "Music", 
                        Datetime= DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    }, 
                    new Performer
                    {
                        PerformerName = "Jay T", 
                        PerformerType = "Poetry", 
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    }, 
                    new Performer
                    {
                        PerformerName = "Jaclyn H.", 
                        PerformerType = "Poetry", 
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    },
                    new Performer
                    {
                        PerformerName = "Gary W",
                        PerformerType = "Music",
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    },
                    new Performer
                    {
                        PerformerName = "Ezbzy",
                        PerformerType = "Comedy", 
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    }, 
                    new Performer
                    {
                        PerformerName = "TK Trio", 
                        PerformerType = "Music", 
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    }, 
                    new Performer
                    {
                        PerformerName = "Adam R.", 
                        PerformerType = "Comedy", 
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    }, 
                    new Performer
                    {
                        PerformerName = "Richard", 
                        PerformerType = "Music", 
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    }, 
                    new Performer
                    {
                        PerformerName = "The Lost Pockets", 
                        PerformerType = "Music", 
                        Datetime= DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    }, 
                    new Performer
                    {
                        PerformerName = "Tucker D", 
                        PerformerType = "Music", 
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    }, 
                    new Performer
                    {
                        PerformerName = "Alan", 
                        PerformerType = "Comedy", 
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")
                    }, 
                    new Performer
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
