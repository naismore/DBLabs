using dblw9.Models;
using dblw9.Services;

namespace dblw9.Handlers
{
    public class ItemInStorageHandler
    {
        private readonly ItemInStorageService _itemInStorageService;

        public ItemInStorageHandler(ItemInStorageService itemInStorageService)
        {
            _itemInStorageService = itemInStorageService;
        }

        public void HandleItemInStorageMenu()
        {
            while (true)
            {
                Menu.ShowItemInStorageMenu(); // Вызываем метод меню для управления товарами
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                        WriteAllItemsInStorage();
                        break;
                    case ConsoleKey.D2:
                        AddItemToStorage();
                        break;
                    case ConsoleKey.D3:
                        EditItemInStorage();
                        break;
                    case ConsoleKey.D4:
                        SearchItemsInStorage();
                        break;
                    case ConsoleKey.D5:
                        RemoveItemFromStorage();
                        break;
                    case ConsoleKey.D0:
                        return; // Возврат в главное меню
                    default:
                        Console.WriteLine("Некорректный выбор. Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void WriteAllItemsInStorage()
        {
            var items = _itemInStorageService.GetAllItemsInStorage();
            foreach (var item in items)
            {
                Console.WriteLine($"ID: {item.Id}, Storage ID: {item.StorageId}, Item ID: {item.ItemId}, Arrival Date: {item.ArrialDate}");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void AddItemToStorage()
        {
            var item = new ItemInStorage();
            Console.Write("Введите ID склада: ");
            item.StorageId = int.Parse(Console.ReadLine());
            Console.Write("Введите ID товара: ");
            item.ItemId = int.Parse(Console.ReadLine());
            Console.Write("Введите дату поступления (yyyy-mm-dd): ");
            item.ArrialDate = DateTime.Parse(Console.ReadLine());

            try
            {
                _itemInStorageService.AddItemToStorage(item);
                Console.WriteLine("Элемент успешно добавлен в хранилище.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void EditItemInStorage()
        {
            Console.Write("Введите ID элемента для редактирования: ");
            var id = int.Parse(Console.ReadLine());
            var existingItem = _itemInStorageService.GetItemById(id).FirstOrDefault();

            if (existingItem == null)
            {
                Console.WriteLine("Элемент не найден.");
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                return;
            }

            Console.Write("Введите новый ID склада (или оставьте пустым для сохранения текущего): ");
            var storageIdInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(storageIdInput))
            {
                existingItem.StorageId = int.Parse(storageIdInput);
            }

            Console.Write("Введите новый ID товара (или оставьте пустым для сохранения текущего): ");
            var itemIdInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(itemIdInput))
            {
                existingItem.ItemId = int.Parse(itemIdInput);
            }

            Console.Write("Введите новую дату поступления (или оставьте пустым для сохранения текущей): ");
            var arrivalDateInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(arrivalDateInput))
            {
                existingItem.ArrialDate = DateTime.Parse(arrivalDateInput);
            }

            try
            {
                _itemInStorageService.UpdateItemInStorage(existingItem);
                Console.WriteLine("Элемент успешно обновлен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void RemoveItemFromStorage()
        {
            Console.Write("Введите ID элемента для удаления: ");
            var id = int.Parse(Console.ReadLine());
            var item = new ItemInStorage { Id = id };

            try
            {
                _itemInStorageService.RemoveItemFromStorage(item);
                Console.WriteLine("Элемент успешно удален из хранилища.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void SearchItemsInStorage()
        {
            Console.Write("Введите ID склада для поиска элементов: ");
            var storageId = int.Parse(Console.ReadLine());
            var items = _itemInStorageService.GetItemsByStorageId(storageId);

            if (items.Count == 0)
            {
                Console.WriteLine("Элементы не найдены.");
            }
            else
            {
                foreach (var item in items)
                {
                    Console.WriteLine($"ID: {item.Id}, Storage ID: {item.StorageId}, Item ID: {item.ItemId}, Arrival Date: {item.ArrialDate}");
                }
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}