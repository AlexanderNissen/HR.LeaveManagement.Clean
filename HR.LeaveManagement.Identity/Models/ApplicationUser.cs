using Microsoft.AspNetCore.Identity;

namespace HR.LeaveManagement.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public bool IsEmailConfirmed { get; set; }
}
