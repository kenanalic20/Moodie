using auth.Models;

namespace Moodie.Data;
public class ActivityRepo:IActivityRepo{
 private readonly ApplicationDbContext _context;
 public ActivityRepo(ApplicationDbContext context){
    _context=context;
 }
 
}