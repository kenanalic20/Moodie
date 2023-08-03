using Moodie.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Moodie.Helper
{
    public class AverageMood
    {
        public void UpdateAverageMood(ApplicationDbContext context, int userId)
        {
            // public void UpdateAverageMood(ApplicationDbContext context, int userId)
            // {
            //     var user = context.Users.Include(u => u.Moods).FirstOrDefault(u => u.Id == userId);
            //
            //     if (user == null || user.Moods.Count == 0)
            //     {
            //         // No moods for the user, set the average mood to 0 or any other default value
            //         user.AverageMood = 0;
            //     }
            //     else
            //     {
            //         double averageMood = user.Moods.Average(m => m.MoodValue);
            //         user.AverageMood = averageMood;
            //     }
            //
            //     context.SaveChanges();
            // }
        }
    }
}
