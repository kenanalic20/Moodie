using Microsoft.AspNetCore.Http;
using System;
using System.IO;
namespace Moodie.Helper;
public class ImageHelper
{
    private readonly string _uploadsFolder;

    public ImageHelper(string uploadsFolder)
    {
        _uploadsFolder = uploadsFolder;
    }

    public string SaveImage(IFormFile imageFile)
    {
        if (imageFile == null)
        {
            return null;
        }

        // Ensure the directory exists
        if (!Directory.Exists(_uploadsFolder))
        {
            Directory.CreateDirectory(_uploadsFolder);
        }

        // Generate a unique file name
        var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
        var filePath = Path.Combine(_uploadsFolder, uniqueFileName);

        // Save the file to the server
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            imageFile.CopyTo(fileStream);
        }

        // Return the relative path
        return "https://localhost:8001/" + Path.Combine("Uploads", "Images", uniqueFileName);
    }
}