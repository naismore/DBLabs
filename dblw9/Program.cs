using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;

namespace dblw9
{
    internal class Program
    {
        const char SELECT_CHAR = '>';
        public static string[] categoriesActions = {
            "Товары",
            "Склады",
        };
        public static string[] itemsActions = { 
            "Найти товар по имени",
            "Найти товар по ID",
            "Переместить товар на другой склад",
            "Удалить данные о товаре"
        };
        public static string[] storageActions =
        {

        };
        public static void Main(string[] args)
        {
            while (true)
            {
                if (!TestConnectDB())
                {
                    Console.WriteLine("Ошибка: Подключение к базе данных отсутствует");
                }
                ShowMenu(categoriesActions);
                var key = Console.ReadKey(true).Key;

                switch(key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("Введите название или ID товара: ");
                        string? itemRawString = Console.ReadLine();
                        if (itemRawString != null)
                        {
                            Console.WriteLine(GetItem(itemRawString));
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
                Console.WriteLine($"{i}. {actions[i]}");
            }
            Console.WriteLine("Нажмите Esc для выхода");
        }

        public static string GetItem(string itemRawString)
        {
            int itemId;
            if (int.TryParse(itemRawString, out itemId))
            {
                return GetItemById(id);
            }
            else
            {
                return GetItemByName(itemRawString);
            }
        }

        
    }
}
