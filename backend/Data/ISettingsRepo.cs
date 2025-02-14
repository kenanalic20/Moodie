﻿using Moodie.Models;

namespace Moodie.Data;

public interface ISettingsRepo
{
    Settings Create(Settings settings, Settings existingSettings);

    Settings GetByUserId(int userId);

    Settings Delete(int userId);

    Settings Update(Settings settings, int userId);
}