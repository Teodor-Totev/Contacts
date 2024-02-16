using System.ComponentModel.DataAnnotations;

namespace Contacts.Models.Contacts
{
	public class ContactFormModel
	{
		[Required(ErrorMessage = "Please enter the first name.")]
		[Display(Name = "First Name")]
		public string FirstName { get; set; } = null!;

		[Required(ErrorMessage = "Please enter the last name.")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; } = null!;

		[Required(ErrorMessage = "Please enter the email.")]
		[EmailAddress(ErrorMessage = "Invalid email format.")]
		[Display(Name = "E-mail")]
		public string Email { get; set; } = null!;

		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; } = null!;

		[Display(Name = "Address")]
		public string Address { get; set; } = null!;

		[Display(Name = "Website")]
		[Url(ErrorMessage = "Invalid URL format.")]
		public string Website { get; set; } = null!;

        public int ContactId { get; set; }
    }
}
