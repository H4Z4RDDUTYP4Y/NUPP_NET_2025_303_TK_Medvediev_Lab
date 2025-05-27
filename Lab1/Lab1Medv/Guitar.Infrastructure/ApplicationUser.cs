using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Guitar.Infrastructure
{
    public class ApplicationUser : IdentityUser
{
    // Додаткові поля за потреби
    public string? FullName { get; set; }
}
}