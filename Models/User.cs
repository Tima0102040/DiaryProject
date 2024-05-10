using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Project.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
 
    public int? RoleId { get; set; }
    public virtual Role Role { get; set; }
    
}