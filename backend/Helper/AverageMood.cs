using Moodie.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using auth.Models;

namespace Moodie.Helper
{
    public class AverageMood
    {
        public void UpdateAverageMood(ApplicationDbContext context, int userId)
        {
            var userMoods = context.Moods.Where(m => m.UserId == userId).ToList();
            if (userMoods.Count > 0)
            {
                double averageMood = userMoods.Average(m => m.MoodValue);
                var mood = context.Moods.Find(userId);
                mood.AverageMood = averageMood;
                context.SaveChanges();
            }
            else
            {
                var mood = context.Moods.Find(userId);
                //get users moodvalue
                mood.AverageMood = mood.MoodValue;
                context.SaveChanges();
            }
        }
    }
}
