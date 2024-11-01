using dblw9.Services;
using Microsoft.IdentityModel.Tokens;

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
                                    System.Console.WriteLine("Товары: ");
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
                                if (!itemRawString.IsNullOrEmpty())
                                {
                                    //List<Item>? items = GetItem<Item>(itemRawString!);
                                };
                                Console.WriteLine("Error!");
                                break;
                        }
                        //Console.WriteLine(GetItem<Item>(itemRawString)); 
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
