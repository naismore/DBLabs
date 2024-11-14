using dblw9.Models;
using dblw9.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace dblw9
{
    public class MyDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Storage> Storages { get; set; } = null!;
        public DbSet<ItemInStorage> ItemsInStorage { get; set; } = null!;
        public DbSet<Supplier> Suppliers { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<ItemInOrder> ItemsInOrder { get; set; } = null!;
        

        public MyDbContext()
        {
            //Database.EnsureDeleted();
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
            modelBuilder.ApplyConfiguration(new ItemInStorageConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new ItemInOrderConfiguration());


            // Связи
            modelBuilder.Entity<Item>().HasOne(i => i.Supplier).WithMany(s => s.Items).HasForeignKey(i => i.SupplierId); // Связь "Один ко многим" между таблицами Товар и Поставщик 
            modelBuilder.Entity<ItemInStorage>().HasOne(iis => iis.Storage).WithMany(s => s.ItemInStorage).HasForeignKey(iis => iis.StorageId); // Связь "Один ко многим" между таблицами Склад и Товары на складе
            modelBuilder.Entity<ItemInStorage>().HasOne(iis => iis.Item).WithMany(i => i.ItemInStorage).HasForeignKey(iis => iis.ItemId); // Связь "Один ко многим" между таблицами Товар и Товары на складе

            // Начальные данные

            // Поставщики
            var apple = new Supplier {Id = 1, Name = "Apple", ContactPersonFirstName = "Name1", ContactPersonLastName = "Lastname1", PhoneNumber = "123123123", Adress = "New York City" };
            var samsung = new Supplier { Id = 2, Name = "Samsung", ContactPersonFirstName = "Ryo", ContactPersonLastName = "Kudo", PhoneNumber = "3213321321", Adress = "Tokyo" };
            var huawei = new Supplier { Id = 3, Name = "Huawei", ContactPersonFirstName = "Ching", ContactPersonLastName = "Chong", PhoneNumber = "31312312", Adress = "Huyung" };
            var xiaomi = new Supplier { Id = 4, Name = "Xiaomi", ContactPersonFirstName = "Chong", ContactPersonLastName = "Chang", PhoneNumber = "12341512", Adress = "GungHuyung" };
            modelBuilder.Entity<Supplier>().HasData(apple, samsung, huawei, xiaomi);

            // Товары
            var item1 = new Item { Id = 1, Name = "Apple iPhone 15 Pro 128GB Dual Sim Blue Titanium", Unit = "шт", Cost = 109999, SupplierId = apple.Id};
            var item2 = new Item { Id = 2, Name = "Apple iPhone 13 128GB nanoSim/eSim Midnight", Unit = "шт", Cost = 59999, SupplierId = apple.Id};
            var item3 = new Item { Id = 3, Name = "Samsung Galaxy A15 LTE 4/128GB Dark Blue", Unit = "шт", Cost = 14999, SupplierId =  samsung.Id};
            var item4 = new Item { Id = 4, Name = "HUAWEI nova 12s 8/256GB Black", Unit = "шт", Cost = 27999, SupplierId = huawei.Id};
            var item5 = new Item { Id = 5, Name = "Xiaomi Redmi Note 13 8/256GB Midnight Black", Unit = "шт", Cost = 20999, SupplierId = xiaomi.Id};
            var item6 = new Item { Id = 6, Name = "Xiaomi Redmi 12 8/256GB Midnight Black", Unit = "шт", Cost = 12999, SupplierId =  xiaomi.Id};
            modelBuilder.Entity<Item>().HasData(item1, item2, item3, item4, item5, item6);

            // Склады
            var storage1 = new Storage { Id = 1, Name = "ST1", Adress = "Moscow", PhoneNumber = "123132131" };
            var storage2 = new Storage { Id = 2, Name = "ST2", Adress = "Vladimir", PhoneNumber = "132313412" };
            var storage3 = new Storage { Id = 3, Name = "ST3", Adress = "Zelenograd", PhoneNumber = "12441212" };
            modelBuilder.Entity<Storage>().HasData(storage1, storage2, storage3);

            // Товары на складах
            var itemsInStorage1 = new ItemInStorage { Id = 6, ItemId = item1.Id, StorageId = storage1.Id, ArrialDate = DateTime.Now};
            var itemsInStorage2 = new ItemInStorage { Id = 1, ItemId = item2.Id, StorageId = storage1.Id, ArrialDate = DateTime.Now };
            var itemsInStorage3 = new ItemInStorage { Id = 2, ItemId = item3.Id, StorageId = storage2.Id, ArrialDate = DateTime.Now };
            var itemsInStorage4 = new ItemInStorage { Id = 3, ItemId = item4.Id, StorageId = storage2.Id, ArrialDate = DateTime.Now };
            var itemsInStorage5 = new ItemInStorage { Id = 4, ItemId = item5.Id, StorageId = storage3.Id, ArrialDate = DateTime.Now };
            var itemsInStorage6 = new ItemInStorage { Id = 5, ItemId = item6.Id, StorageId = storage3.Id, ArrialDate = DateTime.Now };
            modelBuilder.Entity<ItemInStorage>().HasData(itemsInStorage1, itemsInStorage2, itemsInStorage3, itemsInStorage4, itemsInStorage5, itemsInStorage6);

            // Заказы
            var order1 = new Order { Id = 1, CustomerId = 1, OrderDate = DateTime.Now, Adress = "Улица 1" };
            var order2 = new Order { Id = 2, CustomerId = 2, OrderDate = DateTime.Now, Adress = "Улица 2" };
            modelBuilder.Entity<Order>().HasData(order1, order2);

            // Товары в заказах
            var itemInOrder1 = new ItemInOrder { Id = 1, ItemId = 1, OrderId = 1, Quantity = 2 };
            var itemInOrder2 = new ItemInOrder { Id = 2, ItemId = 2, OrderId = 1, Quantity = 1 };
            var itemInOrder3 = new ItemInOrder { Id = 3, ItemId = 1, OrderId = 2, Quantity = 3 };
            modelBuilder.Entity<ItemInOrder>().HasData(itemInOrder1, itemInOrder2, itemInOrder3);

            // Заказчики
            var customer1 = new Customer { Id = 1, FirstName = "Иван", LastName = "Иванов", BirthDate = new DateTime(1990, 1, 1), Email = "ivan@example.com" };
            var customer2 = new Customer { Id = 2, FirstName = "Петр", LastName = "Петров", BirthDate = new DateTime(1985, 5, 5), Email = "petr@example.com" };
            modelBuilder.Entity<Customer>().HasData(customer1, customer2);
        }
    }
}

