using dblw9.Models;
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
                var itemsInStorageService = new ItemInStorageService(new MyDbContext());
                var storageAnalyticsService = new StorageAnalyticsService(new MyDbContext());
                var customerService = new CustomerService(new MyDbContext());
                var orderService = new OrderService(new MyDbContext());
                var itemsInOrderService = new ItemsInOrderService(new MyDbContext());

                ShowMenu(Menu.categoriesActions);
                var key = Console.ReadKey(true).Key;
                string? itemRawString = null;
                string? supplierRawString = null;

                switch (key)
                {
                    case ConsoleKey.D1: // Товары
                        Console.Clear();
                        ShowMenu(Menu.itemStage1);

                        var key1 = Console.ReadKey(true).Key;

                        switch (key1)
                        {
                            case ConsoleKey.D1: // Вывод всех товаров
                                WriteAllItems(itemService);
                                break;
                            case ConsoleKey.D2: // Поиск товара
                                Console.Clear();
                                Console.Write("Введите название или ID товара: ");
                                itemRawString = Console.ReadLine();
                                int result;
                                if (!itemRawString.IsNullOrEmpty())
                                {
                                    if (int.TryParse(itemRawString, out result))
                                    {
                                        GetItem(itemService, result);
                                    }
                                    else
                                    {
                                        SearchItemsByName(itemService, itemRawString);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error!");
                                }
                                break;
                            case ConsoleKey.D3: // Добавление товара
                                AddItem(itemService);
                                break;
                        }
                        break;
                    case ConsoleKey.D2: // Склады
                        Console.Clear();
                        ShowMenu(Menu.storageStage1);
                        var key2 = Console.ReadKey(true).Key;

                        switch (key2)
                        {
                            case ConsoleKey.D1: // Найти склад
                                Console.Clear();
                                Console.Write("Введите название или ID склада: ");
                                string? storageRawString = Console.ReadLine();
                                int result;

                                if (!string.IsNullOrEmpty(storageRawString))
                                {
                                    if (int.TryParse(storageRawString, out result))
                                    {
                                        var storageList = storageService.GetStorageByID(result);
                                        if (storageList != null && storageList.Any())
                                        {
                                            var storage = storageList.First();
                                            Console.WriteLine($"{storage.Id}. {storage.Name}\n\tАдрес: {storage.Adress}\n\tТелефон: {storage.PhoneNumber}");
                                            ShowMenu(Menu.storageStage2);
                                            var key5 = Console.ReadKey(true).Key;

                                            switch (key5)
                                            {
                                                case ConsoleKey.D1: // Удалить склад
                                                    storageService.RemoveStorage(storage);
                                                    Console.WriteLine("Склад успешно удален.");
                                                    break;

                                                case ConsoleKey.D2: // Редактировать склад
                                                    EditStorage(storageService, storage);
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Склад не найден");
                                        }
                                    }
                                    else
                                    {
                                        var storageList = storageService.GetStorageByName(storageRawString!);
                                        if (storageList != null && storageList.Any())
                                        {
                                            foreach (var storage in storageList)
                                            {
                                                Console.WriteLine($"{storage.Id}. {storage.Name}\n\tАдрес: {storage.Adress}\n\tТелефон: {storage.PhoneNumber}");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Склад не найден");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Ошибка! Введите корректные данные.");
                                }
                                break;

                            case ConsoleKey.D2: // Добавить склад
                                var newStorage = new Storage();
                                Console.WriteLine("Введите данные для нового склада:");


                                Console.Write("Название склада: ");
                                newStorage.Name = Console.ReadLine();


                                Console.Write("Адрес склада: ");
                                newStorage.Adress = Console.ReadLine();


                                Console.Write("Номер телефона (не более 13 символов): ");
                                newStorage.PhoneNumber = Console.ReadLine();


                                try
                                {
                                    storageService.AddStorage(newStorage);
                                    Console.WriteLine("Склад успешно добавлен.");
                                }
                                catch (ValidationException vex)
                                {
                                    Console.WriteLine($"Ошибка валидации: {vex.Message}");
                                }
                                break;

                            case ConsoleKey.D3: // Вывести все склады
                                var storages = storageService.GetAllStorages();

                                if (storages != null && storages.Any())
                                {
                                    Console.WriteLine("Склады:");
                                    foreach (var storage in storages)
                                    {
                                        Console.WriteLine($"{storage.Id}. {storage.Name}\n\tАдрес: {storage.Adress}\n\tТелефон: {storage.PhoneNumber}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Склады не найдены");
                                }
                                break;
                        }
                        break;
                    case ConsoleKey.D3: // Поставщики
                        Console.Clear();
                        ShowMenu(Menu.supplierStage1);
                        var key4 = Console.ReadKey(true).Key;
                        switch (key4)
                        {
                            case ConsoleKey.D1: // Найти поставщика
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
                                            ShowMenu(Menu.supplierStage2);

                                            var key5 = Console.ReadKey(true).Key;

                                            switch (key5)
                                            {
                                                case ConsoleKey.D1:
                                                    supplierService.RemoveSupplier(supplier);
                                                    break;
                                                case ConsoleKey.D2:
                                                    UpdateSupplier(supplierService, supplier);
                                                    break;
                                            }

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
                                            foreach (var supplier in supplierList)
                                            {
                                                Console.WriteLine($"{supplier.Id}. {supplier.Name}\n\tОтветственное лицо: {supplier.ContactPersonFirstName} {supplier.ContactPersonLastName}\n\t Номер телефона: {supplier.PhoneNumber}");
                                            }
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
                            case ConsoleKey.D2: // Добавить поставщика
                                AddSupplier(supplierService);
                                break;
                            case ConsoleKey.D3: // Вывести всех поставщиков
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
                    case ConsoleKey.D4:
                        Console.Clear();
                        ShowMenu(Menu.itemsInStorageStage1);
                        var key3 = Console.ReadKey(true).Key;

                        switch (key3)
                        {
                            case ConsoleKey.D1: // Найти элемент в хранилище
                                Console.Clear();
                                Console.Write("Введите ID элемента или ID склада: ");
                                string? searchRawString = Console.ReadLine();
                                int result;

                                if (!string.IsNullOrEmpty(searchRawString))
                                {
                                    if (int.TryParse(searchRawString, out result))
                                    {
                                        var itemList = itemsInStorageService.GetItemById(result);
                                        if (itemList != null && itemList.Any())
                                        {
                                            var item = itemList.First();
                                            Console.WriteLine($"ID: {item.Id}, ID Склада: {item.StorageId}, ID Товара: {item.ItemId}, Дата поступления: {item.ArrialDate}");
                                            ShowMenu(Menu.itemsInStorageStage2);
                                            var key5 = Console.ReadKey(true).Key;

                                            switch (key5)
                                            {
                                                case ConsoleKey.D1: // Удалить элемент
                                                    itemsInStorageService.RemoveItemFromStorage(item);
                                                    Console.WriteLine("Элемент успешно удален.");
                                                    break;

                                                case ConsoleKey.D2: // Редактировать элемент
                                                    Console.WriteLine($"Текущие данные о элементе: ID Склада - {item.StorageId}, ID Товара - {item.ItemId}, Дата поступления - {item.ArrialDate}");
                                                    var updatedItem = new ItemsInStorage { Id = item.Id };


                                                    Console.Write("Новый ID склада (оставьте пустым для сохранения текущего): ");
                                                    var storageIdInput = Console.ReadLine();
                                                    updatedItem.StorageId = string.IsNullOrWhiteSpace(storageIdInput) ? item.StorageId : int.Parse(storageIdInput);

                                                    Console.Write("Новый ID товара (оставьте пустым для сохранения текущего): ");
                                                    var itemIdInput = Console.ReadLine();
                                                    updatedItem.ItemId = string.IsNullOrWhiteSpace(itemIdInput) ? item.ItemId : int.Parse(itemIdInput);

                                                    Console.Write("Новая дата поступления (оставьте пустым для сохранения текущей): ");
                                                    var arrivalDateInput = Console.ReadLine();
                                                    updatedItem.ArrialDate = string.IsNullOrWhiteSpace(arrivalDateInput) ? item.ArrialDate : DateTime.Parse(arrivalDateInput);

                                                    try
                                                    {
                                                        itemsInStorageService.UpdateItemInStorage(updatedItem);
                                                        Console.WriteLine("Элемент успешно обновлен.");
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
                                        }
                                        else
                                        {
                                            Console.WriteLine("Элемент не найден");
                                        }
                                    }
                                    else
                                    {
                                        var itemList = itemsInStorageService.GetItemsByStorageId(result);
                                        if (itemList != null && itemList.Any())
                                        {
                                            foreach (var item in itemList)
                                            {
                                                Console.WriteLine($"ID: {item.Id}, ID Склада: {item.StorageId}, ID Товара: {item.ItemId}, Дата поступления: {item.ArrialDate}");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Элемент не найден");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Ошибка! Введите корректные данные.");
                                }
                                break;

                            case ConsoleKey.D2: // Добавить элемент в хранилище
                                var newItem = new ItemsInStorage();
                                Console.WriteLine("Введите данные для нового элемента:");


                                Console.Write("ID склада: ");
                                newItem.StorageId = int.Parse(Console.ReadLine());


                                Console.Write("ID товара: ");
                                newItem.ItemId = int.Parse(Console.ReadLine());


                                Console.Write("Дата поступления (формат: ГГГГ-ММ-ДД): ");
                                newItem.ArrialDate = DateTime.Parse(Console.ReadLine());

                                try
                                {
                                    itemsInStorageService.AddItemToStorage(newItem);
                                    Console.WriteLine("Элемент успешно добавлен в хранилище.");
                                }
                                catch (ValidationException vex)
                                {
                                    Console.WriteLine($"Ошибка валидации: {vex.Message}");
                                }
                                break;

                            case ConsoleKey.D3: // Вывести все элементы в хранилище
                                var items = itemsInStorageService.GetAllItemsInStorage();

                                if (items != null && items.Any())
                                {
                                    Console.WriteLine("Элементы в хранилище:");
                                    foreach (var item in items)
                                    {
                                        Console.WriteLine($"ID: {item.Id}, ID Склада: {item.StorageId}, ID Товара: {item.ItemId}, Дата поступления: {item.ArrialDate}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Элементы не найдены");
                                }
                                break;
                        }
                        break;
                    case ConsoleKey.D5:
                       
                        var analyticsData = storageAnalyticsService.GetStorageAnalytics();

                        foreach (var data in analyticsData)
                        {
                            Console.WriteLine($"Склад ID: {data.StorageId}, Средняя цена: {data.AveragePrice}, Последняя дата поступления: {data.LatestArrival}, Первая дата поступления: {data.EarliestArrival}, Количество товаров: {data.TotalItems}");
                        }
                        break;
                    case ConsoleKey.D6: // Клиенты
                        Console.Clear();
                        ShowMenu(Menu.customerStage1);
                        var key6 = Console.ReadKey(true).Key;

                        switch (key6)
                        {
                            case ConsoleKey.D1: // Вывести всех клиентов
                                var customers = customerService.GetAllCustomers();
                                if (customers != null && customers.Any())
                                {
                                    Console.WriteLine("Клиенты:");
                                    foreach (var customer in customers)
                                    {
                                        Console.WriteLine($"{customer.Id}. {customer.FirstName} {customer.LastName}, Email: {customer.Email}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Клиенты не найдены.");
                                }
                                break;

                            case ConsoleKey.D2: // Добавить клиента
                                AddCustomer(customerService);
                                break;

                            case ConsoleKey.D3: // Найти клиента
                                Console.Clear();
                                Console.Write("Введите ID или имя клиента: ");
                                string? customerSearch = Console.ReadLine();
                                // Логика поиска клиента по ID или имени
                                break;
                        }
                        break;

                    case ConsoleKey.D7: // Заказы
                        Console.Clear();
                        ShowMenu(Menu.orderStage1);
                        var key7 = Console.ReadKey(true).Key;

                        switch (key7)
                        {
                            case ConsoleKey.D1: // Вывести все заказы
                                var orders = orderService.GetAllOrders();
                                if (orders != null && orders.Any())
                                {
                                    Console.WriteLine("Заказы:");
                                    foreach (var order in orders)
                                    {
                                        Console.WriteLine($"{order.Id}. Адрес: {order.Adress}, Дата: {order.OrderDate}, ID клиента: {order.CustomerId}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Заказы не найдены.");
                                }
                                break;

                            case ConsoleKey.D2: // Добавить заказ
                                AddOrder(orderService);
                                break;

                            case ConsoleKey.D3: // Найти заказ
                                Console.Clear();
                                Console.Write("Введите ID заказа: ");
                                int orderId = int.Parse(Console.ReadLine());
                                // Логика поиска заказа по ID
                                break;
                        }
                        break;

                    case ConsoleKey.D8: // Элементы в заказе
                        Console.Clear();
                        ShowMenu(Menu.itemInOrderStage1);
                        var key8 = Console.ReadKey(true).Key;

                        switch (key8)
                        {
                            case ConsoleKey.D1: // Вывести все элементы в заказе
                                var itemsInOrder = itemsInOrderService.GetAllItemsInOrder();
                                if (itemsInOrder != null && itemsInOrder.Any())
                                {
                                    Console.WriteLine("Элементы в заказах:");
                                    foreach (var itemInOrder in itemsInOrder)
                                    {
                                        Console.WriteLine($"ID: {itemInOrder.Id}, ID товара: {itemInOrder.ItemId}, ID заказа: {itemInOrder.OrderId}, Количество: {itemInOrder.Quantity}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Элементы в заказах не найдены.");
                                }
                                break;

                            case ConsoleKey.D2: // Добавить элемент в заказ
                                AddItemInOrder(itemsInOrderService);
                                break;

                            case ConsoleKey.D3: // Найти элемент в заказе
                                Console.Clear();
                                Console.Write("Введите ID элемента в заказе: ");
                                int itemInOrderId = int.Parse(Console.ReadLine());
                                // Логика поиска элемента в заказе по ID
                                break;
                        }
                        break;
                }
            }
        }

        private static void AddSupplier(SupplierService supplierService)
        {
            var newSupplier = new Supplier();

            Console.WriteLine("Введите данные для нового поставщика:");


            Console.Write("Название поставщика: ");
            newSupplier.Name = Console.ReadLine();

            Console.Write("Имя контактного лица: ");
            newSupplier.ContactPersonFirstName = Console.ReadLine();

            Console.Write("Фамилия контактного лица: ");
            newSupplier.ContactPersonLastName = Console.ReadLine();


            Console.Write("Номер телефона (не более 13 символов): ");
            newSupplier.PhoneNumber = Console.ReadLine();


            Console.Write("Адрес электронной почты (не более 30 символов): ");
            newSupplier.EmailAddress = Console.ReadLine();


            Console.Write("Адрес (не более 50 символов): ");
            newSupplier.Adress = Console.ReadLine();


            try
            {
                supplierService.AddSupplier(newSupplier);
                Console.WriteLine("Поставщик успешно добавлен.");
            }
            catch (ValidationException vex)
            {
                Console.WriteLine($"Ошибка валидации: {vex.Message}");
            }
        }

        private static void UpdateSupplier(SupplierService supplierService, Supplier supplier)
        {
            Console.WriteLine($"Текущие данные о поставщике: Имя - {supplier.Name}, Контактное лицо - {supplier.ContactPersonFirstName} {supplier.ContactPersonLastName}, Телефон - {supplier.PhoneNumber}, Email - {supplier.EmailAddress}, Адрес - {supplier.Adress}");

            var updatedSupplier = new Supplier { Id = supplier.Id };

            Console.Write("Новое имя (оставьте пустым для сохранения текущего): ");
            var nameInput = Console.ReadLine();
            updatedSupplier.Name = string.IsNullOrWhiteSpace(nameInput) ? supplier.Name : nameInput;

            Console.Write("Новое имя контактного лица (оставьте пустым для сохранения текущего): ");
            var firstNameInput = Console.ReadLine();
            updatedSupplier.ContactPersonFirstName = string.IsNullOrWhiteSpace(firstNameInput) ? supplier.ContactPersonFirstName : firstNameInput;

            Console.Write("Новая фамилия контактного лица (оставьте пустым для сохранения текущего): ");
            var lastNameInput = Console.ReadLine();
            updatedSupplier.ContactPersonLastName = string.IsNullOrWhiteSpace(lastNameInput) ? supplier.ContactPersonLastName : lastNameInput;

            Console.Write("Новый номер телефона (оставьте пустым для сохранения текущего): ");
            var phoneInput = Console.ReadLine();
            updatedSupplier.PhoneNumber = string.IsNullOrWhiteSpace(phoneInput) ? supplier.PhoneNumber : phoneInput;

            Console.Write("Новый адрес электронной почты (оставьте пустым для сохранения текущего): ");
            var emailInput = Console.ReadLine();
            updatedSupplier.EmailAddress = string.IsNullOrWhiteSpace(emailInput) ? supplier.EmailAddress : emailInput;

            Console.Write("Новый адрес (оставьте пустым для сохранения текущего): ");
            var addressInput = Console.ReadLine();
            updatedSupplier.Adress = string.IsNullOrWhiteSpace(addressInput) ? supplier.Adress : addressInput;

            try
            {
                supplierService.UpdateSupplier(updatedSupplier);
                Console.WriteLine("Поставщик успешно обновлен.");
            }
            catch (ValidationException vex)
            {
                Console.WriteLine($"Ошибка валидации: {vex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private static void EditStorage(StorageService storageService, Storage storage)
        {
            Console.WriteLine($"Текущие данные о складе: Название - {storage.Name}, Адрес - {storage.Adress}, Телефон - {storage.PhoneNumber}");
            var updatedStorage = new Storage { Id = storage.Id };


            Console.Write("Новое название (оставьте пустым для сохранения текущего): ");
            var nameInput = Console.ReadLine();
            updatedStorage.Name = string.IsNullOrWhiteSpace(nameInput) ? storage.Name : nameInput;

            Console.Write("Новый адрес (оставьте пустым для сохранения текущего): ");
            var addressInput = Console.ReadLine();
            updatedStorage.Adress = string.IsNullOrWhiteSpace(addressInput) ? storage.Adress : addressInput;

            Console.Write("Новый номер телефона (оставьте пустым для сохранения текущего): ");
            var phoneInput = Console.ReadLine();
            updatedStorage.PhoneNumber = string.IsNullOrWhiteSpace(phoneInput) ? storage.PhoneNumber : phoneInput;


            try
            {
                storageService.UpdateStorage(updatedStorage);
                Console.WriteLine("Склад успешно обновлен.");
            }
            catch (ValidationException vex)
            {
                Console.WriteLine($"Ошибка валидации: {vex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private static void AddItem(ItemService itemService)
        {
            var newItem = new Item();

            Console.WriteLine("Введите данные для нового товара:");


            Console.Write("Название товара: ");
            newItem.Name = Console.ReadLine();


            Console.Write("Описание товара (не более 50 символов): ");
            newItem.Description = Console.ReadLine();


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


            Console.Write("Единица измерения (не более 10 символов): ");
            newItem.Unit = Console.ReadLine();


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
        }

        private static void SearchItemsByName(ItemService itemService, string? itemRawString)
        {
            var itemList = itemService.GetItemByName(itemRawString!);
            if (!itemList.IsNullOrEmpty())
            {
                foreach (var item in itemList)
                {
                    Console.WriteLine($"{item.Id}. {item.Name}\n\tЦена: {item.Cost}\n\tОписание: {item.Description}\n");
                }
            }
            else
            {
                Console.WriteLine("Товар не найден");
            }
        }

        private static void GetItem(ItemService itemService, int result)
        {
            var itemList = itemService.GetItemByID(result);
            if (!itemList.IsNullOrEmpty())
            {
                var item = itemList.First();
                Console.WriteLine($"{item.Id}. {item.Name}\n\tЦена: {item.Cost}\n\tОписание: {item.Description}\n");
                ShowMenu(Menu.itemStage2);

                var key5 = Console.ReadKey(true).Key;

                switch (key5)
                {
                    case ConsoleKey.D1: // Удаление
                        RemoveItem(itemService, item);
                        break;
                    case ConsoleKey.D2: // Редактирование
                        EditItem(itemService, item);
                        break;
                }
            }
            else
            {
                Console.WriteLine("Товар не найден");
            }
        }

        private static void EditItem(ItemService itemService, Item item)
        {
            Console.WriteLine($"Текущие данные о товаре: Название - {item.Name}, Описание - {item.Description}, Стоимость - {item.Cost}, Единица измерения - {item.Unit}, ID поставщика - {item.SupplierId}");

            var updatedItem = new Item { Id = item.Id };


            Console.Write("Новое название товара (оставьте пустым для сохранения текущего): ");
            var newName = Console.ReadLine();
            updatedItem.Name = string.IsNullOrWhiteSpace(newName) ? item.Name : newName;


            Console.Write("Новое описание товара (оставьте пустым для сохранения текущего): ");
            var newDescription = Console.ReadLine();
            updatedItem.Description = string.IsNullOrWhiteSpace(newDescription) ? item.Description : newDescription;


            Console.Write("Новая стоимость товара (оставьте пустым для сохранения текущей): ");
            var costInput = Console.ReadLine();
            updatedItem.Cost = string.IsNullOrWhiteSpace(costInput) ? item.Cost : double.Parse(costInput);


            Console.Write("Новая единица измерения (оставьте пустым для сохранения текущей): ");
            var newUnit = Console.ReadLine();
            updatedItem.Unit = string.IsNullOrWhiteSpace(newUnit) ? item.Unit : newUnit;


            Console.Write("Новый ID поставщика (оставьте пустым для сохранения текущего): ");
            var supplierIdInput = Console.ReadLine();
            updatedItem.SupplierId = string.IsNullOrWhiteSpace(supplierIdInput) ? item.SupplierId : int.Parse(supplierIdInput);

            try
            {
                itemService.UpdateItem(updatedItem);
                Console.WriteLine("Товар успешно обновлен.");
            }
            catch (ValidationException vex)
            {
                Console.WriteLine($"Ошибка валидации: {vex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private static void RemoveItem(ItemService itemService, Item item)
        {
            try
            {
                itemService.RemoveItem(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            };
        }

        private static void WriteAllItems(ItemService itemService)
        {
            Console.Clear();
            var items = itemService.GetAllItems();
            if (!items.IsNullOrEmpty())
            {
                Console.WriteLine("Товары: ");
                foreach (var item in items)
                {
                    Console.WriteLine($"{item.Id}. {item.Name}\n\tЦена: {item.Cost}\n\tОписание: {item.Description}\n");
                }
            }
            else
            {
                Console.WriteLine("Товары не найдены");
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

        private static void AddCustomer(CustomerService customerService)
        {
            var newCustomer = new Customer();

            Console.WriteLine("Введите данные для нового клиента:");

            Console.Write("Имя: ");
            newCustomer.FirstName = Console.ReadLine();

            Console.Write("Фамилия: ");
            newCustomer.LastName = Console.ReadLine();

            Console.Write("Дата рождения (формат: ГГГГ-ММ-ДД): ");
            newCustomer.BirthDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Email: ");
            newCustomer.Email = Console.ReadLine();

            try
            {
                customerService.AddCustomer(newCustomer);
                Console.WriteLine("Клиент успешно добавлен.");
            }
            catch (ValidationException vex)
            {
                Console.WriteLine($"Ошибка валидации: {vex.Message}");
            }
        }
        private static void UpdateCustomer(CustomerService customerService, Customer customer)
        {
            Console.WriteLine($"Текущие данные о клиенте: Имя - {customer.FirstName}, Фамилия - {customer.LastName}, Дата рождения - {customer.BirthDate}, Email - {customer.Email}");

            var updatedCustomer = new Customer { Id = customer.Id };

            Console.Write("Новое имя (оставьте пустым для сохранения текущего): ");
            var firstNameInput = Console.ReadLine();
            updatedCustomer.FirstName = string.IsNullOrWhiteSpace(firstNameInput) ? customer.FirstName : firstNameInput;

            Console.Write("Новая фамилия (оставьте пустым для сохранения текущего): ");
            var lastNameInput = Console.ReadLine();
            updatedCustomer.LastName = string.IsNullOrWhiteSpace(lastNameInput) ? customer.LastName : lastNameInput;

            Console.Write("Новая дата рождения (оставьте пустым для сохранения текущей): ");
            var birthDateInput = Console.ReadLine();
            updatedCustomer.BirthDate = string.IsNullOrWhiteSpace(birthDateInput) ? customer.BirthDate : DateTime.Parse(birthDateInput);

            Console.Write("Новый Email (оставьте пустым для сохранения текущего): ");
            var emailInput = Console.ReadLine();
            updatedCustomer.Email = string.IsNullOrWhiteSpace(emailInput) ? customer.Email : emailInput;

            try
            {
                customerService.UpdateCustomer(updatedCustomer);
                Console.WriteLine("Клиент успешно обновлен.");
            }
            catch (ValidationException vex)
            {
                Console.WriteLine($"Ошибка валидации: {vex.Message}");
            }
        }

        private static void RemoveCustomer(CustomerService customerService, Customer customer)
        {
            try
            {
                customerService.RemoveCustomer(customer);
                Console.WriteLine("Клиент успешно удален.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private static void AddOrder(OrderService orderService)
        {
            var newOrder = new Order();

            Console.WriteLine("Введите данные для нового заказа:");

            Console.Write("Адрес: ");
            newOrder.Adress = Console.ReadLine();

            Console.Write("Дата заказа (формат: ГГГГ-ММ-ДД): ");
            newOrder.OrderDate = DateTime.Parse(Console.ReadLine());

            Console.Write("ID клиента: ");
            newOrder.CustomerId = int.Parse(Console.ReadLine());

            try
            {
                orderService.AddOrder(newOrder);
                Console.WriteLine("Заказ успешно добавлен.");
            }
            catch (ValidationException vex)
            {
                Console.WriteLine($"Ошибка валидации: {vex.Message}");
            }
        }

        private static void UpdateOrder(OrderService orderService, Order order)
        {
            Console.WriteLine($"Текущие данные о заказе: Адрес - {order.Adress}, Дата заказа - {order.OrderDate}, ID клиента - {order.CustomerId}");

            var updatedOrder = new Order { Id = order.Id };

            Console.Write("Новый адрес (оставьте пустым для сохранения текущего): ");
            var addressInput = Console.ReadLine();
            updatedOrder.Adress = string.IsNullOrWhiteSpace(addressInput) ? order.Adress : addressInput;

            Console.Write("Новая дата заказа (оставьте пустым для сохранения текущей): ");
            var orderDateInput = Console.ReadLine();
            updatedOrder.OrderDate = string.IsNullOrWhiteSpace(orderDateInput) ? order.OrderDate : DateTime.Parse(orderDateInput);

            Console.Write("Новый ID клиента (оставьте пустым для сохранения текущего): ");
            var customerIdInput = Console.ReadLine();
            updatedOrder.CustomerId = string.IsNullOrWhiteSpace(customerIdInput) ? order.CustomerId : int.Parse(customerIdInput);

            try
            {
                orderService.UpdateOrder(updatedOrder);
                Console.WriteLine("Заказ успешно обновлен.");
            }
            catch (ValidationException vex)
            {
                Console.WriteLine($"Ошибка валидации: {vex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private static void RemoveOrder(OrderService orderService, Order order)
        {
            try
            {
                orderService.RemoveOrder(order);
                Console.WriteLine("Заказ успешно удален.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private static void AddItemInOrder(ItemsInOrderService itemInOrderService)
        {
            var newItemInOrder = new ItemsInOrder();

            Console.WriteLine("Введите данные для нового элемента в заказе:");

            Console.Write("ID товара: ");
            newItemInOrder.ItemId = int.Parse(Console.ReadLine());

            Console.Write("ID заказа: ");
            newItemInOrder.OrderId = int.Parse(Console.ReadLine());

            Console.Write("Количество: ");
            newItemInOrder.Quantity = int.Parse(Console.ReadLine());

            try
            {
                itemInOrderService.AddItemInOrder(newItemInOrder);
                Console.WriteLine("Элемент успешно добавлен в заказ.");
            }
            catch (ValidationException vex)
            {
                Console.WriteLine($"Ошибка валидации: {vex.Message}");
            }
        }

        private static void UpdateItemInOrder(ItemsInOrderService itemInOrderService, ItemsInOrder itemInOrder)
        {
            Console.WriteLine($"Текущие данные о элементе в заказе: ID товара - {itemInOrder.ItemId}, ID заказа - {itemInOrder.OrderId}, Количество - {itemInOrder.Quantity}");

            var updatedItemInOrder = new ItemsInOrder { Id = itemInOrder.Id };

            Console.Write("Новый ID товара (оставьте пустым для сохранения текущего): ");
            var itemIdInput = Console.ReadLine();
            updatedItemInOrder.ItemId = string.IsNullOrWhiteSpace(itemIdInput) ? itemInOrder.ItemId : int.Parse(itemIdInput);

            Console.Write("Новый ID заказа (оставьте пустым для сохранения текущего): ");
            var orderIdInput = Console.ReadLine();
            updatedItemInOrder.OrderId = string.IsNullOrWhiteSpace(orderIdInput) ? itemInOrder.OrderId : int.Parse(orderIdInput);

            Console.Write("Новое количество (оставьте пустым для сохранения текущего): ");
            var quantityInput = Console.ReadLine();
            updatedItemInOrder.Quantity = string.IsNullOrWhiteSpace(quantityInput) ? itemInOrder.Quantity : int.Parse(quantityInput);

            try
            {
                itemInOrderService.UpdateItemInOrder(updatedItemInOrder);
                Console.WriteLine("Элемент в заказе успешно обновлен.");
            }
            catch (ValidationException vex)
            {
                Console.WriteLine($"Ошибка валидации: {vex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private static void RemoveItemInOrder(ItemsInOrderService itemInOrderService, ItemsInOrder itemInOrder)
        {
            try
            {
                itemInOrderService.RemoveItemInOrder(itemInOrder);
                Console.WriteLine("Элемент успешно удален из заказа.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
