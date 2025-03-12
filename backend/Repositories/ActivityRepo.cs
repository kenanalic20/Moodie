using Moodie.Models;
using Moodie.Interfaces;
using Moodie.Data;
using Microsoft.EntityFrameworkCore;


namespace Moodie.Repositories;
public class ActivityRepo : IActivityRepo
{
   private readonly ApplicationDbContext _context;
   public ActivityRepo(ApplicationDbContext context)
   {
      _context = context;
   }
   public Activity Create(Activity activity)
   {
      _context.Activity.Add(activity);
      _context.SaveChanges();
      return activity;
   }

   public Activity Update(Activity activity)
   {
      _context.Activity.Update(activity);
      _context.SaveChanges();
      return activity;
   }

   public List<Activity> GetByUserId(int UserId)
   {
      var data = _context.Activity.Where(a => a.UserId == UserId).ToList();
      return data;
   }

   public Activity GetById(int? Id)
   {
      var activity = _context.Activity.Find(Id);
      return activity;
   }

   public void Delete(int Id)
   {
      var activity = _context.Activity.Find(Id);
      _context.Activity.Remove(activity);
      _context.SaveChanges();
     
   }


}