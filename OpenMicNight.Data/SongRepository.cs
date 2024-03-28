using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OpenMicNight.Domain;

namespace OpenMicNight.Data
{
    public class SongRepository : ISongRepository
    {
        private readonly PerformerContext _dbContext;
        private readonly PerformerRepository _performanceRepository;

        public SongRepository(PerformerContext dbContext)
        {
            _dbContext = new PerformerContext();
        }
        public void AddSong(Song song)
        {
            _dbContext.Songs.Add(song);
            _dbContext.SaveChanges();
        }
        public void RemoveSong(Song song)
        {
            _dbContext.Songs.Remove(song);
            _dbContext.SaveChanges();
        }
        public void UpdateSong(Song song)
        {
            _dbContext.Songs.Update(song);
            _dbContext.SaveChanges();
        }
        public List<Song> GetAllSongs()
        {
            return _dbContext.Songs.ToList();
        }
        public List<Song> GetAllSongsByPerformerId(int id)
        {
            return _dbContext.Songs
                .FromSql($"SELECT * FROM [Songs] WHERE PerformerId = {id}")
                .ToList();
        }
    }
}