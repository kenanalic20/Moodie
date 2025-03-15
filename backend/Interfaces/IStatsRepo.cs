using Moodie.Models;
namespace Moodie.Interfaces;

public interface IStatsRepo
{
   List<Statistics> GetByUserID(int userId);
}