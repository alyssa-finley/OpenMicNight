using OpenMicNight.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMicNight.Data
{
    public interface IMusicRepository
    {
        void AddMusic(Music music);
        void RemoveMusic(Music music);
        void UpdateMusic(Music music);

        Music GetMusicByPerformerId(int id);
        List<Music> GetAllMusic();
        List<Music> GetMusicByPerformerName(string name);
        List<Music> GetMusicBySong(string type);
        void SeedMusicWithPerformances();

    }
}
