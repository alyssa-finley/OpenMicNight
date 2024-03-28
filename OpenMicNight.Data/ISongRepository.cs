using OpenMicNight.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMicNight.Data
{
    public interface ISongRepository
    {
        void AddSong(Song song);
        void RemoveSong(Song song);
        void UpdateSong(Song song);

        List<Song> GetAllSongs();
        List<Song> GetAllSongsByPerformerId(int id);

    }
}
