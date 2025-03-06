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
      var data = _context.Activity.Include(a=>a.Mood).Where(a => a.UserId == UserId).ToList();
      return data;
   }

   public List<Activity> GetBestMoodActivities(double averageMoodValue,int UserId)
   {
      var userActivities = GetByUserId(UserId);
      return userActivities.Where(a =>a.Mood !=null && a.Mood.MoodValue >= averageMoodValue).OrderByDescending(m=>m.Mood.MoodValue).ToList();
   }

   public List<Activity> GetWorstMoodActivities(double averageMoodValue,int UserId)
   {
      var userActivities = GetByUserId(UserId);
      return userActivities.Where(a =>a.Mood !=null && a.Mood.MoodValue < averageMoodValue && a.UserId==UserId).OrderByDescending(m=>m.Mood.MoodValue).ToList();
   }

   public Activity GetById(int? Id, List<Activity> activities)
   {
      var activity = activities.Where(a=>Id!=null && a.Id==Id).FirstOrDefault();
      return activity;
   }

   public void Delete(int Id)
   {
      var activity = _context.Activity.Find(Id);
      _context.Activity.Remove(activity);
      _context.SaveChanges();
     
   }


}