using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Contacts.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            ApplicationUsersContacts = new HashSet<ApplicationUserContact>();
        }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public override string UserName { get; set; } = null!;

        [Required]
        [StringLength(60, MinimumLength = 10)]
        public override string Email { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Password { get; set; } = null!;

        public ICollection<ApplicationUserContact> ApplicationUsersContacts { get; set; }
    }
}
