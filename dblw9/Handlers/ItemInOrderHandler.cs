using dblw9.Models;
using dblw9.Services;

namespace dblw9.Handlers
{
    public class ItemInOrderHandler
    {
        private readonly ItemInOrderService _itemInOrderService;

        public ItemInOrderHandler(ItemInOrderService itemInOrderService)
        {
            _itemInOrderService = itemInOrderService;
        }

        public void HandleItemInOrderMenu()
        {
            while (true)
            {
                Menu.ShowItemInOrderMenu(); // Вызываем метод меню для управления элементами в заказах
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                        WriteAllItemsInOrder();
                        break;
                    case ConsoleKey.D2:
                        AddItemInOrder();
                        break;
                    case ConsoleKey.D3:
                        EditItemInOrder();
                        break;
                    case ConsoleKey.D4:
                        RemoveItemInOrder();
                        break;
                    case ConsoleKey.D5:
                        SearchItemsInOrder();
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

        private void WriteAllItemsInOrder()
        {
            var itemsInOrder = _itemInOrderService.GetAllItemsInOrder();
            foreach (var item in itemsInOrder)
            {
                Console.WriteLine($"ID: {item.Id}, Item ID: {item.ItemId}, Order ID: {item.OrderId}, Quantity: {item.Quantity}");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void AddItemInOrder()
        {
            var itemInOrder = new ItemInOrder();
            Console.Write("Введите ID товара: ");
            itemInOrder.ItemId = int.Parse(Console.ReadLine());
            Console.Write("Введите ID заказа: ");
            itemInOrder.OrderId = int.Parse(Console.ReadLine());
            Console.Write("Введите количество: ");
            itemInOrder.Quantity = int.Parse(Console.ReadLine());

            try
            {
                _itemInOrderService.AddItemInOrder(itemInOrder);
                Console.WriteLine("Элемент успешно добавлен в заказ.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void EditItemInOrder()
        {
            Console.Write("Введите ID элемента в заказе для редактирования: ");
            var id = int.Parse(Console.ReadLine());
            var existingItemInOrder = _itemInOrderService.GetAllItemsInOrder().FirstOrDefault(io => io.Id == id);

            if (existingItemInOrder == null)
            {
                Console.WriteLine("Элемент в заказе не найден.");
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                return;
            }

            Console.Write("Введите новый ID товара (или оставьте пустым для сохранения текущего): ");
            var itemIdInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(itemIdInput))
            {
                existingItemInOrder.ItemId = int.Parse(itemIdInput);
            }

            Console.Write("Введите новый ID заказа (или оставьте пустым для сохранения текущего): ");
            var orderIdInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(orderIdInput))
            {
                existingItemInOrder.OrderId = int.Parse(orderIdInput);
            }

            Console.Write("Введите новое количество (или оставьте пустым для сохранения текущего): ");
            var quantityInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(quantityInput))
            {
                existingItemInOrder.Quantity = int.Parse(quantityInput);
            }

            try
            {
                _itemInOrderService.UpdateItemInOrder(existingItemInOrder);
                Console.WriteLine("Элемент в заказе успешно обновлен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void RemoveItemInOrder()
        {
            Console.Write("Введите ID элемента в заказе для удаления: ");
            var id = int.Parse(Console.ReadLine());
            var itemInOrder = new ItemInOrder { Id = id };

            try
            {
                _itemInOrderService.RemoveItemInOrder(itemInOrder);
                Console.WriteLine("Элемент успешно удален из заказа.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void SearchItemsInOrder()
        {
            Console.Write("Введите ID заказа для поиска элементов: ");
            var orderId = int.Parse(Console.ReadLine());
            var itemsInOrder = _itemInOrderService.GetItemsInOrderByOrderId(orderId);

            if (itemsInOrder.Count == 0)
            {
                Console.WriteLine("Элементы в заказе не найдены.");
            }
            else
            {
                foreach (var item in itemsInOrder)
                {
                    Console.WriteLine($"ID: {item.Id}, Item ID: {item.ItemId}, Quantity: {item.Quantity}");
                }
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}