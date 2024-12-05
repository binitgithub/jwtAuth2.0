using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwtAuth2._0.Models
{
    public class User
    {
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
    }
}