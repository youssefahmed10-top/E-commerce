using System.Reflection.Metadata;

namespace Presistence.Data
{
    public class StoreDbContext:DbContext
    {

        public StoreDbContext(DbContextOptions<StoreDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //1:modelBuilder.ApplyConfiguration(new ProductConfigurations());
            //2:modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //3:
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssmblyReference).Assembly);
        }

        public DbSet<Product> products { get; set; }
        public DbSet<ProductBrand> productBrands { get; set; }
        public DbSet<ProductType> productTypes { get; set; }
    }
}
