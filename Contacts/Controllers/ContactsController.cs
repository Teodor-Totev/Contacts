using Contacts.Data;
using Contacts.Data.Models;
using Contacts.Models.Contacts;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ContactsDbContext context;

        public ContactsController(ContactsDbContext context)
        {
            this.context = context;
        }

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

        public async Task<IActionResult> All()
        {

        }
    }
}
