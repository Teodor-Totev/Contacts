namespace Contacts.Models.Contacts
{
    public class ContactVM
    {
        public ContactVM()
        {
            Contacts = new HashSet<ContactFormModel>();
        }

        public ICollection<ContactFormModel> Contacts { get; set; }
    }
}
