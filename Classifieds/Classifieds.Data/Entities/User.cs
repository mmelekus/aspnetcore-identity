using Microsoft.AspNetCore.Identity;

namespace Classifieds.Data.Entities;

public class User : IdentityUser
{
    public DateTime DateOfBirth { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}