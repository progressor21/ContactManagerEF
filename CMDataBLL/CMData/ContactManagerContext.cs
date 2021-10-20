using CMDataBLL.Models;
using Microsoft.EntityFrameworkCore;

namespace CMDataBLL.CMData
{
    public class ContactManagerContext : DbContext
    {
        public ContactManagerContext(DbContextOptions<ContactManagerContext> options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactEmailAddress> ContactEmailAddresses { get; set; }
    }
}