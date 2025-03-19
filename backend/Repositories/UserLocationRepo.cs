using Moodie.Models;
using Moodie.Interfaces;
using Moodie.Data;



namespace Moodie.Repositories;

public class UserLocationRepo : IUserLocationRepo
{
    private readonly ApplicationDbContext _context;

    public UserLocationRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public UserLocation Create(UserLocation userLocation)
    {
        _context.UserLocations.Add(userLocation);
        _context.SaveChanges();
        return userLocation;
    }


    public void Delete(int userId)
    {
        var existingUserLocation = _context.UserLocations.FirstOrDefault(userImage => userImage.UserId == userId);
       if(existingUserLocation!=null) {
            _context.UserLocations.Remove(existingUserLocation);
            _context.SaveChanges();
       }

    }

    public UserLocation Update(UserLocation userLocation)
    {
        var existingUserLocation = _context.UserLocations.Find(userLocation.Id);
        if (existingUserLocation != null)
        {
            existingUserLocation.Country = userLocation.Country;
            existingUserLocation.Province = userLocation.Province;
            existingUserLocation.City = userLocation.City;

            _context.SaveChanges();
        }
        return existingUserLocation;
    }

    public UserLocation GetByUserId(int userId)
    {
        return _context.UserLocations
        .FirstOrDefault(ui => ui.UserId == userId);
    }
}