using dblw9.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace dblw9
{
    internal class MyDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Storage> Storages { get; set; } = null!;
        public DbSet<ItemsInStorage> ItemsInStorages { get; set; } = null!;
        public DbSet<Supplier> Suppliers { get; set; } = null!;

        public MyDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=dblw9;Trusted_Connection=True;TrustServerCertificate=True;");
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new StorageConfiguration());
            modelBuilder.ApplyConfiguration(new ItemsInStorageConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());


            // Связи
            modelBuilder.Entity<Item>().HasOne(i => i.Supplier).WithMany(s => s.Items).HasForeignKey(i => i.SupplierId); // Связь "Один ко многим" между таблицами Товар и Поставщик 
            modelBuilder.Entity<ItemsInStorage>().HasOne(iis => iis.Storage).WithMany(s => s.ItemsInStorages).HasForeignKey(iis => iis.StorageId); // Связь "Один ко многим" между таблицами Склад и Товары на складе
            modelBuilder.Entity<ItemsInStorage>().HasOne(iis => iis.Item).WithMany(i => i.ItemsInStorages).HasForeignKey(iis => iis.ItemId); // Связь "Один ко многим" между таблицами Товар и Товары на складе
        }
    }
}

