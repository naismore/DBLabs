using dblw9.Models;
using dblw9.Services;
using System.ComponentModel.DataAnnotations;

namespace dblw9.Handlers
{
    public class OrderHandler
    {
        private readonly OrderService _orderService;

        public OrderHandler(OrderService orderService)
        {
            _orderService = orderService;
        }

        public void HandleOrderMenu()
        {
            Console.Clear();
            Menu.ShowOrderMenu();
            var key1 = Console.ReadKey(true).Key;

            switch (key1)
            {
                case ConsoleKey.D1:
                    WriteAllOrders();
                    Console.ReadKey();
                    break;
                case ConsoleKey.D2:
                    AddOrder();
                    break;
                case ConsoleKey.D3:
                    EditOrder();
                    break;
                case ConsoleKey.D4:
                    SearchOrders(); // Добавляем вызов метода поиска
                    break;
                case ConsoleKey.D5:
                    RemoveOrder(); // Добавляем вызов метода удаления
                    break;
            }
        }

        private void WriteAllOrders()
        {
            Console.Clear();
            var orders = _orderService.GetAllOrders();
            if (orders != null && orders.Any())
            {
                Console.WriteLine("Заказы: ");
                foreach (var order in orders)
                {
                    Console.WriteLine($"{order.Id}. Заказ от {order.OrderDate.ToShortDateString()} для клиента {order.Customer.FirstName} {order.Customer.LastName}\n\tАдрес: {order.Adress}\n");
                }
            }
            else
            {
                Console.WriteLine("Заказы не найдены");
            }
        }

        private void AddOrder()
        {
            var newOrder = new Order();

            Console.WriteLine("Введите данные для нового заказа:");

            Console.Write("Адрес: ");
            newOrder.Adress = Console.ReadLine();

            Console.Write("Дата заказа (yyyy-mm-dd): ");
            newOrder.OrderDate = DateTime.Parse(Console.ReadLine());

            Console.Write("ID клиента: ");
            newOrder.CustomerId = int.Parse(Console.ReadLine());

            try
            {
                _orderService.AddOrder(newOrder);
                Console.WriteLine("Заказ успешно добавлен.");
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

        private void EditOrder()
        {
            Console.Write("Введите ID заказа для редактирования: ");
            if (int.TryParse(Console.ReadLine(), out int orderId))
            {
                var order = _orderService.GetOrderByID(orderId).FirstOrDefault();
                if (order != null)
                {
                    Console.WriteLine($"Текущие данные о заказе: Адрес - {order.Adress}, Дата - {order.OrderDate.ToShortDateString()}, ID клиента - {order.CustomerId}");

                    Console.Write("Новый адрес (оставьте пустым для сохранения текущего): ");
                    var newAddress = Console.ReadLine();
                    order.Adress = string.IsNullOrWhiteSpace(newAddress) ? order.Adress : newAddress;

                    Console.Write("Новая дата заказа (оставьте пустым для сохранения текущей): ");
                    var orderDateInput = Console.ReadLine();
                    order.OrderDate = string.IsNullOrWhiteSpace(orderDateInput) ? order.OrderDate : DateTime.Parse(orderDateInput);

                    Console.Write("Новый ID клиента (оставьте пустым для сохранения текущего): ");
                    var customerIdInput = Console.ReadLine();
                    order.CustomerId = string.IsNullOrWhiteSpace(customerIdInput) ? order.CustomerId : int.Parse(customerIdInput);

                    try
                    {
                        _orderService.UpdateOrder(order);
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
                else
                {
                    Console.WriteLine("Заказ с таким ID не найден.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод ID.");
            }
        }

        private void SearchOrders()
        {
            Console.Write("Введите ID клиента для поиска заказов: ");
            if (int.TryParse(Console.ReadLine(), out int customerId))
            {
                var foundOrders = _orderService.GetOrdersByCustomerId(customerId);
                if (foundOrders != null && foundOrders.Any())
                {
                    Console.WriteLine("Найденные заказы: ");
                    foreach (var order in foundOrders)
                    {
                        Console.WriteLine($"{order.Id}. Заказ от {order.OrderDate.ToShortDateString()} для клиента {order.Customer!.FirstName} {order.Customer.LastName}\n\tАдрес: {order.Adress}\n");
                    }
                }
                else
                {
                    Console.WriteLine("Заказы не найдены для данного клиента.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод ID клиента.");
            }
        }

        private void RemoveOrder()
        {
            Console.Write("Введите ID заказа для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int orderId))
            {
                var order = _orderService.GetOrderByID(orderId).FirstOrDefault();
                if (order != null)
                {
                    try
                    {
                        _orderService.RemoveOrder(order);
                        Console.WriteLine("Заказ успешно удален.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Заказ с таким ID не найден.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод ID.");
            }
        }
    }
}