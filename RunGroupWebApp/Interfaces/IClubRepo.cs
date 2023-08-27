using RunGroupWebApp.Models;

namespace RunGroupWebApp.Interfaces
{
    public interface IClubRepo
    {
        bool Add(Club club);
        bool Delete(Club club);
        bool Update(Club club);
        bool Save();
        Task<IEnumerable<Club>> GetAllClub();
        Task<Club>GetClubById(int id);
        Task<IEnumerable<Club>>GetClubsByCity(string cityName);
    }
}
