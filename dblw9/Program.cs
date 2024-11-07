using dblw9.Services;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;

namespace dblw9
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            while (true)
            {
                if (!TestConnectDB())
                {
                    Console.WriteLine("Ошибка: Подключение к базе данных отсутствует");
                    break;
                }

                var itemService = new ItemService(new MyDbContext());
                var storageService = new StorageService(new MyDbContext());
                var supplierService = new SupplierService(new MyDbContext());
                var iisSerice = new ItemInStorageService(new MyDbContext());

                ShowMenu(Menu.categoriesActions);
                var key = Console.ReadKey(true).Key;
                string? itemRawString = null;
                string? supplierRawString = null;

                switch (key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        ShowMenu(Menu.itemStage1);

                        var key1 = Console.ReadKey(true).Key;

                        switch (key1)
                        {
                            case ConsoleKey.D1:
                                Console.Clear();
                                var items = itemService.GetAllItems();
                                if (!items.IsNullOrEmpty())
                                {
                                    Console.WriteLine("Товары: ");
                                    foreach(var item in items)
                                    {
                                        Console.WriteLine($"{item.Id}. {item.Name}\n\tЦена: {item.Cost}\n\tОписание: {item.Description}\n");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Товары не найдены");
                                }
                                break;
                            case ConsoleKey.D2:
                                Console.Clear();
                                Console.Write("Введите название или ID товара: ");
                                itemRawString = Console.ReadLine();
                                int result;
                                if (!itemRawString.IsNullOrEmpty())
                                {
                                    if (int.TryParse(itemRawString, out result))
                                    {
                                        var itemList = itemService.GetItemByID(result);
                                        if (!itemList.IsNullOrEmpty())
                                        {
                                            var item = itemList.First();
                                            Console.WriteLine($"{item.Id}. {item.Name}\n\tЦена: {item.Cost}\n\tОписание: {item.Description}\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Товар не найден");
                                        }
                                    }
                                    else
                                    {
                                        var itemList = itemService.GetItemByName(itemRawString!);
                                        if (!itemList.IsNullOrEmpty())
                                        {
                                            var item = itemList.First();
                                            Console.WriteLine($"{item.Id}. {item.Name}\n\tЦена: {item.Cost}\n\tОписание: {item.Description}\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Товар не найден");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error!");
                                }
                                break;
                            case ConsoleKey.D3:
                                var newItem = new Item();

                                Console.WriteLine("Введите данные для нового товара:");

                                // Запрос имени
                                Console.Write("Название товара: ");
                                newItem.Name = Console.ReadLine();

                                // Запрос описания
                                Console.Write("Описание товара (не более 50 символов): ");
                                newItem.Description = Console.ReadLine();

                                // Запрос стоимости
                                double cost;
                                while (true)
                                {
                                    Console.Write("Стоимость товара: ");
                                    if (double.TryParse(Console.ReadLine(), out cost) && cost >= 0)
                                    {
                                        newItem.Cost = cost;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Пожалуйста, введите корректное значение стоимости.");
                                    }
                                }

                                // Запрос единицы измерения
                                Console.Write("Единица измерения (не более 10 символов): ");
                                newItem.Unit = Console.ReadLine();

                                // Запрос ID поставщика
                                int supplierId;
                                while (true)
                                {
                                    Console.Write("ID поставщика: ");
                                    if (int.TryParse(Console.ReadLine(), out supplierId))
                                    {
                                        newItem.SupplierId = supplierId;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Пожалуйста, введите корректное значение ID поставщика.");
                                    }
                                }

                                // Добавление товара в базу данных
                                try
                                {
                                    itemService.AddItem(newItem);
                                    Console.WriteLine("Товар успешно добавлен.");
                                }
                                catch (ValidationException vex)
                                {
                                    Console.WriteLine($"Ошибка валидации: {vex.Message}");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Ошибка: {ex.Message}");
                                }
                                break;
                            }
                        break;
                    case ConsoleKey.D2:
                        
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        ShowMenu(Menu.supplierStage1);
                        var key2 = Console.ReadKey(true).Key;
                        switch(key2)
                        {
                            case ConsoleKey.D1:
                                Console.Clear();
                                Console.Write("Введите название или ID поставщика: ");
                                supplierRawString = Console.ReadLine();
                                int result;
                                if (!supplierRawString.IsNullOrEmpty())
                                {
                                    if (int.TryParse(supplierRawString, out result))
                                    {
                                        var supplierList = supplierService.GetSupplierByID(result);
                                        if (!supplierList.IsNullOrEmpty())
                                        {
                                            var supplier = supplierList.First();
                                            Console.WriteLine($"{supplier.Id}. {supplier.Name}\n\tОтветственное лицо: {supplier.ContactPersonFirstName} {supplier.ContactPersonLastName}\n\t Номер телефона: {supplier.PhoneNumber}");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Поставщик не найден");
                                        }
                                    }
                                    else
                                    {
                                        var supplierList = supplierService.GetSupplierByName(supplierRawString!);
                                        if (!supplierList.IsNullOrEmpty())
                                        {
                                            var supplier = supplierList.First();
                                            Console.WriteLine($"{supplier.Id}. {supplier.Name}\n\tОтветственное лицо: {supplier.ContactPersonFirstName} {supplier.ContactPersonLastName}\n\t Номер телефона: {supplier.PhoneNumber}");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Товар не найден");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error!");
                                }
                                break;
                            case ConsoleKey.D2:
                                var newSupplier = new Supplier();

                                Console.WriteLine("Введите данные для нового поставщика:");

                                // Запрос имени поставщика
                                Console.Write("Название поставщика: ");
                                newSupplier.Name = Console.ReadLine();

                                // Запрос имени контактного лица
                                Console.Write("Имя контактного лица: ");
                                newSupplier.ContactPersonFirstName = Console.ReadLine();

                                // Запрос фамилии контактного лица
                                Console.Write("Фамилия контактного лица: ");
                                newSupplier.ContactPersonLastName = Console.ReadLine();

                                // Запрос номера телефона
                                Console.Write("Номер телефона (не более 13 символов): ");
                                newSupplier.PhoneNumber = Console.ReadLine();

                                // Запрос адреса электронной почты
                                Console.Write("Адрес электронной почты (не более 30 символов): ");
                                newSupplier.EmailAddress = Console.ReadLine();

                                // Запрос адреса
                                Console.Write("Адрес (не более 50 символов): ");
                                newSupplier.Adress = Console.ReadLine();

                                // Добавление поставщика в базу данных
                                try
                                {
                                    supplierService.AddSupplier(newSupplier);
                                    Console.WriteLine("Поставщик успешно добавлен.");
                                }
                                catch (ValidationException vex)
                                {
                                    Console.WriteLine($"Ошибка валидации: {vex.Message}");
                                }
                                break;
                            case ConsoleKey.D3:
                                var suppliers = supplierService.GetAllSuppliers();
                                if (!suppliers.IsNullOrEmpty())
                                {
                                    System.Console.WriteLine("Поставщики: ");
                                    foreach (var supplier in suppliers)
                                    {
                                        Console.WriteLine($"{supplier.Id}. {supplier.Name}\n\tОтветственное лицо: {supplier.ContactPersonFirstName} {supplier.ContactPersonLastName}\n\t Номер телефона: {supplier.PhoneNumber}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Поставщики не найдены");
                                }
                                break;
                        }
                        break;
                }
            }
        }

        public static bool TestConnectDB()
        {
            using (MyDbContext db = new())
            {
                return db.Database.CanConnect();
            }
        }

        public static void ShowMenu(string[] actions)
        {
            Console.WriteLine("Выберите действие: ");
            for (int i = 0; i < actions.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {actions[i]}");
            }
            Console.WriteLine("Нажмите Esc для выхода");
        }
    }
}
