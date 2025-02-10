using System;

namespace API.Entities;

public class AppUser
{
    public int ID { get; set; }

    public required string UserName { get; set; }
    public required byte[] PasswordHash { get; set; }

     public required byte[] PasswordSalt { get; set; }
}


