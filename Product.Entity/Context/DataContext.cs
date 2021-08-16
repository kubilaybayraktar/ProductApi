using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Api
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
               : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CategoryAttribute>().HasKey(x => new { x.CategoryId, x.AttributeId});
            modelBuilder.Entity<ProductAttribute>().HasKey(x => new { x.ProductId, x.AttributeId});
            modelBuilder.Entity<CategoryAttributeValue>().HasNoKey();
            modelBuilder.Entity<StringScalar>().HasNoKey();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<LookupCategory> Categories { get; set; }
        public DbSet<LookupAttribute> Attributes { get; set; }
        public DbSet<CategoryAttribute> CategoryAttributes { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<StringScalar> StringValues { get; set; }
        public DbSet<CategoryAttributeValue> CategoryAttributeValues { get; set; }
    }

    public class StringScalar
    {
        public string Value { get; set; }
    }
}
