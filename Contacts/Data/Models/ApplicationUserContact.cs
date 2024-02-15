using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts.Data.Models
{
    public class ApplicationUserContact
    {
        public Guid ApplicationUserId { get; set; } 

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;

        public int ContactId { get; set; }

        [ForeignKey(nameof(ContactId))]
        public Contact Contact { get; set; } = null!;
    }
}