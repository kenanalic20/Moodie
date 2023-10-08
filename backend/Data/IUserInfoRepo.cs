﻿using auth.Models;
using Microsoft.AspNetCore.Mvc;

namespace Moodie.Data;

public interface IUserInfoRepo
{
    UserInfo Create(UserInfo userInfo,int userId);
    List<UserInfo> GetByUserId(int userId);
  
    void Delete(int userId);
}