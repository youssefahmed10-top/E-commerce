using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Presistence.Identity
{
    public class IdentityStoreDbContext : IdentityDbContext
    {
        public IdentityStoreDbContext(DbContextOptions<IdentityStoreDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Address>().ToTable("Address");
        }
    }
}
