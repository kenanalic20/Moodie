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

        if (!Directory.Exists(_uploadsFolder))
        {
            Directory.CreateDirectory(_uploadsFolder);
        }

        var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
        var filePath = Path.Combine(_uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            imageFile.CopyTo(fileStream);
        }

        return "https://localhost:8001/" + Path.Combine("Uploads", "Images", uniqueFileName);
    }

    public string UpdateImage(IFormFile newImageFile, string existingImagePath)
    {
        if (newImageFile == null)
        {
            return null;
        }
        if (string.IsNullOrEmpty(existingImagePath))
        {
            return null;
        }

        DeleteImage(existingImagePath);

        return SaveImage(newImageFile);
    }

     public bool DeleteImage(string imagePath)
    {
        if (string.IsNullOrEmpty(imagePath))
        {
            return false;
        }

        var filePath = Path.Combine(_uploadsFolder, Path.GetFileName(imagePath));

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return true;
        }

        return false;
    }
}