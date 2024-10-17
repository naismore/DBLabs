using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;

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
                }
                ShowMenu(Actions.categoriesActions);
                var key = Console.ReadKey(true).Key;

                switch(key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("Введите название или ID товара: ");
                        string? itemRawString = Console.ReadLine();
                        if (itemRawString != null)
                        {
                            Console.WriteLine(GetItem<Item>(itemRawString));
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

        public static List<T> GetItem<T>(string rawString)
        {
            int itemId;
            if (int.TryParse(rawString, out itemId))
            {
                return GetById<T>(itemId);
            }
            else
            {
                return GetByName(rawString);
            }
        }

        public static List<T> GetById<T>(int id)
        {
            List<T> result = new();
            using (MyDbContext db = new())
            { 
                if (typeof(T) == typeof(Item))
                { 
                    var items = (from item in db.Items where item.Id == id select item).ToList();
                    foreach(Item item in items)
                    {
                        result.Add((T)(object)item);
                    } 
                }
                else if (typeof(T) == typeof(Storage))
                {
                    var storages = (from storage in db.Storages where storage.Id == id select storage).ToList();
                    foreach(Storage storage in storages)
                    {
                        result.Add((T)(object)storage);
                    }
                }
            }
            return result;
        }

        public static List<T> GetByName<T>(string name)
        {
            
        }
    }
}
