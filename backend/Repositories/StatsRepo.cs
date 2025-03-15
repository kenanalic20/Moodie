using Moodie.Data;
using Moodie.Interfaces;
using Moodie.Models;

namespace Moodie.Repositories;
public class StatsRepo : IStatsRepo
{
    private readonly ApplicationDbContext _context;
    public StatsRepo(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<Statistics> GetByUserID(int userId)
    {
        return _context.Statistics.Where(u => u.UserId == userId).ToList();
    }
}