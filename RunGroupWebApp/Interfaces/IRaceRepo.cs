using RunGroupWebApp.Models;

namespace RunGroupWebApp.Interfaces
{
    public interface IRaceRepo
    {
        bool Add(Race race);
        bool Remove(Race race);
        bool Update(Race race);
        bool Save();
        Task<Race> GetRaceById(int id);
        Task <IEnumerable<Race>> GetAllRaces();
        Task <IEnumerable<Race>> GetAllracesByCity(string city);
    }
}
