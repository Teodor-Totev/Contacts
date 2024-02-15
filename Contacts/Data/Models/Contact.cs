using System.ComponentModel.DataAnnotations;

namespace Contacts.Data.Models
{
    public class Contact
    {
        public Contact()
        {
            ApplicationUsersContacts = new HashSet<ApplicationUserContact>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(60, MinimumLength = 10)]
        public string Email { get; set; } = null!;

        [Required]
        [RegularExpression(@"^(0\s[0-9]{3}\s[0-9]{2}\s[0-9]{2}|(\+359-[0-9]{3}-[0-9]{2}-[0-9]{2}-[0-9]{2}))$")]
        public string PhoneNumber { get; set; } = null!;

        public string? Address { get; set; }

        [Required]
        [RegularExpression(@"^www\.[a-zA-Z0-9-]*\.bg$")]
        public string Website { get; set; } = null!;

        public ICollection<ApplicationUserContact> ApplicationUsersContacts { get; set; }
    }
}
