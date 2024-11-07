using dblw9.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace dblw9
{
    public class MyDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Storage> Storages { get; set; } = null!;
        public DbSet<ItemsInStorage> ItemsInStorages { get; set; } = null!;
        public DbSet<Supplier> Suppliers { get; set; } = null!;

        public MyDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=dblw9;Trusted_Connection=True;TrustServerCertificate=True;");
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

            // Начальные данные

            // Поставщики
            Supplier apple = new Supplier {Id = -1, Name = "Apple", ContactPersonFirstName = "Name1", ContactPersonLastName = "Lastname1", PhoneNumber = "123123123", Adress = "New York City" };
            Supplier samsung = new Supplier { Id = -2, Name = "Samsung", ContactPersonFirstName = "Ryo", ContactPersonLastName = "Kudo", PhoneNumber = "3213321321", Adress = "Tokyo" };
            Supplier huawei = new Supplier { Id = -3, Name = "Huawei", ContactPersonFirstName = "Ching", ContactPersonLastName = "Chong", PhoneNumber = "31312312", Adress = "Huyung" };
            Supplier xiaomi = new Supplier { Id = -4, Name = "Xiaomi", ContactPersonFirstName = "Chong", ContactPersonLastName = "Chang", PhoneNumber = "12341512", Adress = "GungHuyung" };
            modelBuilder.Entity<Supplier>().HasData(apple, samsung, huawei, xiaomi);

            // Товары
            Item item1 = new Item { Id = -1, Name = "Apple iPhone 15 Pro 128GB Dual Sim Blue Titanium", Unit = "шт", Cost = 109999, SupplierId = apple.Id};
            Item item2 = new Item { Id = -6, Name = "Apple iPhone 13 128GB nanoSim/eSim Midnight", Unit = "шт", Cost = 59999, SupplierId = apple.Id};
            Item item3 = new Item { Id = -2, Name = "Samsung Galaxy A15 LTE 4/128GB Dark Blue", Unit = "шт", Cost = 14999, SupplierId =  samsung.Id};
            Item item4 = new Item { Id = -3, Name = "HUAWEI nova 12s 8/256GB Black", Unit = "шт", Cost = 27999, SupplierId = huawei.Id};
            Item item5 = new Item { Id = -4, Name = "Xiaomi Redmi Note 13 8/256GB Midnight Black", Unit = "шт", Cost = 20999, SupplierId = xiaomi.Id};
            Item item6 = new Item { Id = -5, Name = "Xiaomi Redmi 12 8/256GB Midnight Black", Unit = "шт", Cost = 12999, SupplierId =  xiaomi.Id};
            modelBuilder.Entity<Item>().HasData(item1, item2, item3, item4, item5, item6);

            // Склады
            Storage storage1 = new Storage { Id = -1, Name = "ST1", Adress = "Moscow", PhoneNumber = "123132131" };
            Storage storage2 = new Storage { Id = -2, Name = "ST2", Adress = "Vladimir", PhoneNumber = "132313412" };
            Storage storage3 = new Storage { Id = -3, Name = "ST3", Adress = "Zelenograd", PhoneNumber = "12441212" };
            modelBuilder.Entity<Storage>().HasData(storage1, storage2, storage3);

            // Товары на складах
            ItemsInStorage itemsInStorage1 = new ItemsInStorage { Id = -6, ItemId = item1.Id, StorageId = storage1.Id, ArrialDate = DateTime.Now};
            ItemsInStorage itemsInStorage2 = new ItemsInStorage { Id = -1, ItemId = item2.Id, StorageId = storage1.Id, ArrialDate = DateTime.Now };
            ItemsInStorage itemsInStorage3 = new ItemsInStorage { Id = -2, ItemId = item3.Id, StorageId = storage2.Id, ArrialDate = DateTime.Now };
            ItemsInStorage itemsInStorage4 = new ItemsInStorage { Id = -3, ItemId = item4.Id, StorageId = storage2.Id, ArrialDate = DateTime.Now };
            ItemsInStorage itemsInStorage5 = new ItemsInStorage { Id = -4, ItemId = item5.Id, StorageId = storage3.Id, ArrialDate = DateTime.Now };
            ItemsInStorage itemsInStorage6 = new ItemsInStorage { Id = -5, ItemId = item6.Id, StorageId = storage3.Id, ArrialDate = DateTime.Now };
            modelBuilder.Entity<ItemsInStorage>().HasData(itemsInStorage1, itemsInStorage2, itemsInStorage3, itemsInStorage4, itemsInStorage5, itemsInStorage6);

        }
    }
}

