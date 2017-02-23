using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApplication.dal
{
    public class  DatabaseContext: DbContext
    {
        public DatabaseContext()
            : base("DefaultConnection")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Education> Education { get; set; }
    }
}
