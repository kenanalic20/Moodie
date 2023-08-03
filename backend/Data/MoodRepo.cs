﻿using auth.Models;

namespace Moodie.Data
{
    public class MoodRepo : IMoodRepo
    {
        private readonly ApplicationDbContext _context;
        public MoodRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public Mood Create(Mood mood)
        {
            _context.Moods.Add(mood);
            mood.Id = _context.SaveChanges();
            return mood;
        }
        public Mood GetById(int id)
        {
            return _context.Moods.FirstOrDefault(u => u.Id == id);
        }
        
        public List<Mood> GetByUserId(int userId)
        {
            return _context.Moods.Where(u => u.UserId == userId).ToList();
        }
    }
}

