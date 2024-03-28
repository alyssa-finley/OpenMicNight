using OpenMicNight.Domain;

namespace OpenMicNight.Data
{
    public interface IPerformerRepository
    {
        void AddPerformer(Performer performer);  
        void RemovePerformer(Performer performer);
        void UpdatePerformer(Performer performer);
        Performer GetPerformersById(int id);
        List<Performer> GetAllPerformers();
        List<Performer> GetPerformersByName(string name);
        List<Performer> GetPerformersByType(string type);
        void SeedPerformers();
    }
}
