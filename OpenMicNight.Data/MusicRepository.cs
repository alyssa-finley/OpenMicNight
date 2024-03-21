using Microsoft.EntityFrameworkCore;
using OpenMicNight.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMicNight.Data
{
    public class MusicRepository : IMusicRepository
    {
        private readonly PerformanceContext _dbContext;
        private readonly PerformanceRepository _performanceRepository;
        public MusicRepository()
        {
            _dbContext = new PerformanceContext();
        }
    public void AddMusic(Music music)
        {
            _dbContext.Music.Add(music);
            _dbContext.SaveChanges();
        }
    public void RemoveMusic(Music music)
        {
            _dbContext.Music.Remove(music);
            _dbContext.SaveChanges();
        }
    public void UpdateMusic(Music music)
        {
            _dbContext.Music.Update(music);
            _dbContext.SaveChanges();
        }
    public List<Music> GetAllMusic()
        {
            return _dbContext.Music.ToList();
        }
    public Music GetMusicByPerformerId(int id)
        {
            return _dbContext.Music.SingleOrDefault(x => x.PerformerId == id);
        }
    public List<Music> GetMusicByPerformerName (string name)
        {
            return _dbContext.Music
                .FromSql($"SELECT * FROM [Music] WHERE PerformerName = {name}")
                .ToList();
        }
    public List<Music> GetMusicBySong (string songName)
        {
            return _dbContext.Music
                .FromSql($"SELECT * FROM [Music] WHERE Songs = {songName}")
                .ToList();
        }
    public void SeedMusicWithPerformances()
        {
            if (!_dbContext.Music.Any())
            {
                var performer1 = new Performance
                {
                    PerformerName = "Two For One",
                    PerformerType = "Music",
                    Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                _performanceRepository.AddPerformance(performer1);

                _dbContext.Music.AddRange(
                    new Music
                    {
                        PerformerId = performer1.PerformerId,
                        PerformerName = performer1.PerformerName,
                        Performance = performer1,
                        Songs = new List<Song>
                        {
                            new Song
                            {
                                SongName = "Little Things",
                                IsOriginal = true
                            },
                            new Song
                            {
                                SongName = "Just the Two Of Us",
                                IsOriginal = false
                            },
                            new Song
                            {
                                SongName = "Ghost",
                                IsOriginal = true
                            }
                        }
                    });
                _dbContext.SaveChanges();

                var performer2 = new Performance
                {
                    PerformerName = "Mama Said", 
                    PerformerType = "Music",
                    Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                _performanceRepository.AddPerformance(performer2);

                _dbContext.Music.AddRange(
                   new Music
                   {
                       PerformerId = performer2.PerformerId,
                       PerformerName = performer2.PerformerName,
                       Performance = performer2,
                       Songs = new List<Song>
                       {
                            new Song
                            {
                                SongName = "First Song",
                                IsOriginal = true
                            },
                            new Song
                            {
                                SongName = "Two",
                                IsOriginal = false
                            },
                            new Song
                            {
                                SongName = "Another Name",
                                IsOriginal = true
                            }
                       }
                   });

                //I need to add songs for the remaining 6 performers 
                _dbContext.SaveChanges();
            }
            }
        }
    
}


