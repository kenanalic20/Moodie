using Moodie.Models;

namespace Moodie.Data;

public interface ISettingsRepo
{
    Settings Create(Settings settings, int userId);

    Settings GetByUserId(int userId);

    Settings Delete(int userId);
}