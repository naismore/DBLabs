using System;

namespace dblw9
{
    public static class Menu
    {
        public static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Управление клиентами");
            Console.WriteLine("2. Управление заказами");
            Console.WriteLine("3. Управление поставщиками");
            Console.WriteLine("4. Управление складами");
            Console.WriteLine("5. Управление товарами");
            Console.WriteLine("6. Управление товарами на складе");
            Console.WriteLine("7. Управление товарами в заказе");
            Console.WriteLine("0. Выход");
        }

        public static void ShowCustomerMenu()
        {
            Console.Clear();
            Console.WriteLine("Управление клиентами:");
            Console.WriteLine("1. Просмотреть всех клиентов");
            Console.WriteLine("2. Добавить клиента");
            Console.WriteLine("3. Редактировать клиента");
            Console.WriteLine("4. Искать клиентов");
            Console.WriteLine("5. Удалить клиента");
            Console.WriteLine("0. Назад");
        }

        public static void ShowOrderMenu()
        {
            Console.Clear();
            Console.WriteLine("Управление заказами:");
            Console.WriteLine("1. Просмотреть все заказы");
            Console.WriteLine("2. Добавить заказ");
            Console.WriteLine("3. Редактировать заказ");
            Console.WriteLine("4. Искать заказы");
            Console.WriteLine("5. Удалить заказ");
            Console.WriteLine("0. Назад");
        }

        public static void ShowSupplierMenu()
        {
            Console.Clear();
            Console.WriteLine("Управление поставщиками:");
            Console.WriteLine("1. Просмотреть всех поставщиков");
            Console.WriteLine("2. Добавить поставщика");
            Console.WriteLine("3. Редактировать поставщика");
            Console.WriteLine("4. Искать поставщиков");
            Console.WriteLine("5. Удалить поставщика");
            Console.WriteLine("0. Назад");
        }

        public static void ShowStorageMenu()
        {
            Console.Clear();
            Console.WriteLine("Управление складами:");
            Console.WriteLine("1. Просмотреть все склады");
            Console.WriteLine("2. Добавить склад");
            Console.WriteLine("3. Редактировать склад");
            Console.WriteLine("4. Искать склады");
            Console.WriteLine("5. Удалить склад");
            Console.WriteLine("0. Назад");
        }

        public static void ShowItemMenu()
        {
            Console.Clear();
            Console.WriteLine("Управление товарами:");
            Console.WriteLine("1. Просмотреть все товары");
            Console.WriteLine("2. Добавить товар");
            Console.WriteLine("3. Редактировать товар");
            Console.WriteLine("4. Искать товары");
            Console.WriteLine("5. Удалить товар");
            Console.WriteLine("0. Назад");
        }

        public static void ShowItemInOrderMenu()
        {
            Console.Clear();
            Console.WriteLine("Меню управления элементами в заказах:");
            Console.WriteLine("1. Показать все элементы в заказах");
            Console.WriteLine("2. Добавить элемент в заказ");
            Console.WriteLine("3. Редактировать элемент в заказе");
            Console.WriteLine("4. Удалить элемент из заказа");
            Console.WriteLine("5. Найти элементы по ID заказа");
            Console.WriteLine("0. Вернуться в главное меню");
            Console.Write("Выберите опцию: ");
        }

        public static void ShowItemInStorageMenu()
        {
            Console.Clear();
            Console.WriteLine("Меню управления элементами в хранилище:");
            Console.WriteLine("1. Показать все элементы в хранилище");
            Console.WriteLine("2. Добавить элемент в хранилище");
            Console.WriteLine("3. Редактировать элемент в хранилище");
            Console.WriteLine("4. Удалить элемент из хранилища");
            Console.WriteLine("5. Найти элементы по ID склада");
            Console.WriteLine("0. Вернуться в главное меню");
            Console.Write("Выберите опцию: ");
        }
    }
}