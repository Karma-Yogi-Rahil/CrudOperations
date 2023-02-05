using CrudOperations.Models;
using Microsoft.EntityFrameworkCore;

namespace ByTheBooksWeb.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option):base(option) 
        {

        }    

        public DbSet<UserDetails> UserDetails { get; set; }
    }
}
