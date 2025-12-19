using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapp04.Models;

namespace webapp04.Data
{
    public class webapp04Context : DbContext
    {
        public webapp04Context (DbContextOptions<webapp04Context> options)
            : base(options)
        {
        }

        public DbSet<webapp04.Models.Student> Student { get; set; } = default!;
        
        public DbSet<webapp04.Models.Department> Department { get; set; } = default!;
        public DbSet<webapp04.Models.Course> Course { get; set; } = default!;
    }
}
