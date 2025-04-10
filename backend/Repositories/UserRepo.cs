﻿using Moodie.Models;
using Moodie.Interfaces;
using Moodie.Data;

namespace Moodie.Repositories;

public class UserRepo : IUserRepo
{
    private readonly ApplicationDbContext _context;

    public UserRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public User Create(User user)
    {
        _context.Users.Add(user);
        user.Id = _context.SaveChanges();
        return user;
    }

    public User GetByEmail(string email)
    {
        return _context.Users.Where(u => u.Email == email).FirstOrDefault();
    }

    public User GetById(int id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }

    public User GetByEmailToken(string token)
    {
        return _context.Users.FirstOrDefault(u => u.EmailToken == token);
    }

    public User Update(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
        return user;
    }

    public User GetByUsername(string username)
    {
        return _context.Users.FirstOrDefault(u => u.Username == username);
    }
    
}