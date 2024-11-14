using dblw9.Models;
using dblw9.Services;
using System.ComponentModel.DataAnnotations;

namespace dblw9.Handlers
{
    public class ItemHandler
    {
        private readonly ItemService _itemService;

        public ItemHandler(ItemService itemService)
        {
            _itemService = itemService;
        }

        public void HandleItemMenu()
        {
            Console.Clear();
            Menu.ShowItemMenu();
            var key1 = Console.ReadKey(true).Key;

            switch (key1)
            {
                case ConsoleKey.D1:
                    WriteAllItems();
                    Console.ReadKey();
                    break;
                case ConsoleKey.D2:
                    AddItem();
                    break;
                case ConsoleKey.D3:
                    EditItem();
                    break;
                case ConsoleKey.D4:
                    SearchItems(); // Добавляем вызов метода поиска
                    break;
                case ConsoleKey.D5:
                    RemoveItem(); // Добавляем вызов метода удаления
                    break;
            }
        }

        private void WriteAllItems()
        {
            Console.Clear();
            var items = _itemService.GetAllItems();
            if (items != null && items.Any())
            {
                Console.WriteLine("Товары: ");
                foreach (var item in items)
                {
                    Console.WriteLine($"{item.Id}. {item.Name}\n\tОписание: {item.Description}\n\tСтоимость: {item.Cost}\n");
                }
            }
            else
            {
                Console.WriteLine("Товары не найдены");
            }
        }

        private void AddItem()
        {
            var newItem = new Item();

            Console.WriteLine("Введите данные для нового товара:");

            Console.Write("Название товара: ");
            newItem.Name = Console.ReadLine();

            Console.Write("Описание товара: ");
            newItem.Description = Console.ReadLine();

            Console.Write("Стоимость товара: ");
            newItem.Cost = double.Parse(Console.ReadLine());

            Console.Write("Единица измерения: ");
            newItem.Unit = Console.ReadLine();

            Console.Write("ID поставщика: ");
            newItem.SupplierId = int.Parse(Console.ReadLine());

            try
            {
                _itemService.AddItem(newItem);
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

        private void EditItem()
        {
            Console.Write("Введите ID товара для редактирования: ");
            if (int.TryParse(Console.ReadLine(), out int itemId))
            {
                var item = _itemService.GetItemByID(itemId).FirstOrDefault();
                if (item != null)
                {
                    Console.WriteLine($"Текущие данные о товаре: Название - {item.Name}, Описание - {item.Description}, Стоимость - {item.Cost}, Единица измерения - {item.Unit}, ID поставщика - {item.SupplierId}");

                    Console.Write("Новое название товара (оставьте пустым для сохранения текущего): ");
                    var newName = Console.ReadLine();
                    item.Name = string.IsNullOrWhiteSpace(newName) ? item.Name : newName;

                    Console.Write("Новое описание товара (оставьте пустым для сохранения текущего): ");
                    var newDescription = Console.ReadLine();
                    item.Description = string.IsNullOrWhiteSpace(newDescription) ? item.Description : newDescription;

                    Console.Write("Новая стоимость товара (оставьте пустым для сохранения текущей): ");
                    var costInput = Console.ReadLine();
                    item.Cost = string.IsNullOrWhiteSpace(costInput) ? item.Cost : double.Parse(costInput);

                    Console.Write("Новая единица измерения (оставьте пустым для сохранения текущей): ");
                    var newUnit = Console.ReadLine();
                    item.Unit = string.IsNullOrWhiteSpace(newUnit) ? item.Unit : newUnit;

                    Console.Write("Новый ID поставщика (оставьте пустым для сохранения текущего): ");
                    var supplierIdInput = Console.ReadLine();
                    item.SupplierId = string.IsNullOrWhiteSpace(supplierIdInput) ? item.SupplierId : int.Parse(supplierIdInput);

                    try
                    {
                        _itemService.UpdateItem(item);
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
                else
                {
                    Console.WriteLine("Товар с таким ID не найден.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод ID.");
            }
        }

        private void SearchItems()
        {
            Console.Write("Введите название товара для поиска: ");
            var searchTerm = Console.ReadLine();

            var foundItems = _itemService.GetItemByName(searchTerm);
            if (foundItems != null && foundItems.Any())
            {
                Console.WriteLine("Найденные товары: ");
                foreach (var item in foundItems)
                {
                    Console.WriteLine($"{item.Id}. {item.Name}\n\tОписание: {item.Description}\n\tСтоимость: {item.Cost}\n");
                }
            }
            else
            {
                Console.WriteLine("Товары не найдены.");
            }
        }

        private void RemoveItem()
        {
            Console.Write("Введите ID товара для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int itemId))
            {
                var item = _itemService.GetItemByID(itemId).FirstOrDefault();
                if (item != null)
                {
                    try
                    {
                        _itemService.RemoveItem(item);
                        Console.WriteLine("Товар успешно удален.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Товар с таким ID не найден.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод ID.");
            }
        }
    }
}