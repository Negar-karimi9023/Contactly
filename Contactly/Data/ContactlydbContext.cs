using Contactly.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Contactly.Data
{
    public class ContactlydbContext : DbContext
    {
        public ContactlydbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
