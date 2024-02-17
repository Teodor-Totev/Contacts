using Contacts.Data;
using Contacts.Data.Models;
using Contacts.Models.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Contacts.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ContactsDbContext context;

        public ContactsController(ContactsDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ContactFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Contact c = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Website = model.Website,
            };

            await context.Contacts.AddAsync(c);
            await context.SaveChangesAsync();

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var contact = await context.Contacts
                .FindAsync(id);

            if (contact == null)
            {
                return BadRequest();
            }

            var model = new ContactFormModel()
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                Address = contact.Address ?? string.Empty,
                Website = contact.Website,
                ContactId = contact.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ContactFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var contact = await context.Contacts
               .FindAsync(id);

            if (contact == null)
            {
                return BadRequest();
            }

            contact.FirstName = model.FirstName;
            contact.LastName = model.LastName;
            contact.Email = model.Email;
            contact.Address = model.Address;
            contact.PhoneNumber = model.PhoneNumber;
            contact.Website = model.Website;

            await context.SaveChangesAsync();
            return RedirectToAction("All");
        }

        public async Task<IActionResult> All()
        {
            var allContacts = await context.Contacts
                .Select(x => new ContactFormModel
                {
                    ContactId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Address = x.Address,
                    PhoneNumber = x.PhoneNumber,
                    Website = x.Website
                })
                .ToArrayAsync();

            ContactVM model = new ContactVM()
            {
                Contacts = allContacts,
            };

            return View(model);
        }

        public async Task<IActionResult> Team()
        {
            var userId = Guid.Parse(GetUserId());

            var allContacts = await context.ApplicationUsersContacts
                .Where(u => u.ApplicationUserId == userId)
                .Select(x => new ContactFormModel
                {
                    ContactId = x.Contact.Id,
                    FirstName = x.Contact.FirstName,
                    LastName = x.Contact.LastName,
                    Email = x.Contact.Email,
                    Address = x.Contact.Address ?? string.Empty,
                    PhoneNumber = x.Contact.PhoneNumber,
                    Website = x.Contact.Website
                })
                .ToArrayAsync();

            ContactVM model = new ContactVM()
            {
                Contacts = allContacts,
            };

            return View(model);
        }

        public async Task<IActionResult> AddToTeam(int id)
        {
            var userId = Guid.Parse(GetUserId());
            var contactId = id;

            var ac = new ApplicationUserContact()
            {
                ApplicationUserId = userId,
                ContactId = contactId,
            };

            if (await context.ApplicationUsersContacts.ContainsAsync(ac))
            {
                return RedirectToAction("All");
            }

            await context.ApplicationUsersContacts.AddAsync(ac);
            await context.SaveChangesAsync();

            return RedirectToAction("All");
        }

        public async Task<IActionResult> RemoveFromTeam(int id)
        {
            var userId = Guid.Parse(GetUserId());

            var ac = await context.ApplicationUsersContacts.FirstOrDefaultAsync(x => x.ContactId == id && x.ApplicationUserId == userId);

            if (ac == null)
            {
                return RedirectToAction("Team");
            }

            context.ApplicationUsersContacts.Remove(ac);
            await context.SaveChangesAsync();

            return RedirectToAction("Team");
        }

        protected string GetUserId()
            => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    }
}
