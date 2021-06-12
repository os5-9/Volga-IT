using System.Data.Entity;

namespace Volga_IT.Models
{
    class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("name=DefaultConnection")
        {
        }
        public DbSet<UniqueWord> UniqueWords { get; set; }
    }
}
