using Microsoft.AspNetCore.Identity;

namespace TebanRM.Application.Entities;
public class TebanUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public ICollection<Contact>? Contacts { get; set; }
    public ICollection<CommunicationSchedule>? CommunicationSchedules { get; set; }
}
