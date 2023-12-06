using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace auth.Models;

public class UserImage
{
    public int Id { get; set; }
    public byte[]? Image { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }

    [ForeignKey("UserInfoId")] public int UserInfoId { get; set; }

    [JsonIgnore] public UserInfo UserInfo { get; set; }
}