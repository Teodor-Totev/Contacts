using Contacts.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Data
{
    public class ContactsDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ContactsDbContext(DbContextOptions<ContactsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; } = null!;

        public DbSet<ApplicationUserContact> ApplicationUsersContacts { get; set; } = null!;

        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUserContact>()
                .HasKey(ac => new { ac.ApplicationUserId, ac.ContactId });

            builder.Entity<ApplicationUserContact>()
                .HasOne(ac => ac.Contact)
                .WithMany()
                .HasForeignKey(ac => ac.ContactId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ApplicationUserContact>()
                .HasOne(ac => ac.ApplicationUser)
                .WithMany()
                .HasForeignKey(ac => ac.ApplicationUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
               .Entity<Contact>()
               .HasData(new Contact()
               {
                   Id = 1,
                   FirstName = "Bruce",
                   LastName = "Wayne",
                   PhoneNumber = "+359881223344",
                   Address = "Gotham City",
                   Email = "imbatman@batman.com",
                   Website = "www.batman.com"
               });

            base.OnModelCreating(builder);
        }
    }
}