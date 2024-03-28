using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenMicNight.Data;
using OpenMicNight.Domain;

namespace OpenMicNight.Logic
{
    public interface ISongLogic
    {
        void AddSong(Song song);
        void RemoveSong(Song song);
        void UpdateSong(Song song);
        List<Song> GetAllSongs();
    }
}
