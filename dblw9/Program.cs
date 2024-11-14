using dblw9.Handlers;
using dblw9.Services;

namespace dblw9
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new MyDbContext();
            var customerService = new CustomerService(context);
            var orderService = new OrderService(context);
            var supplierService = new SupplierService(context);
            var storageService = new StorageService(context);
            var itemService = new ItemService(context);
            var itemInStorageService = new ItemInStorageService(context);
            var itemInOrderService = new ItemInOrderService(context);

            var customerHandler = new CustomerHandler(customerService);
            var orderHandler = new OrderHandler(orderService);
            var supplierHandler = new SupplierHandler(supplierService);
            var storageHandler = new StorageHandler(storageService);
            var itemHandler = new ItemHandler(itemService);
            var itemInStorageHandler = new ItemInStorageHandler(itemInStorageService);
            var itemInOrderHandler = new ItemInOrderHandler(itemInOrderService);
            

            while (true)
            {
                Menu.ShowMainMenu();

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                        customerHandler.HandleCustomerMenu();
                        break;
                    case ConsoleKey.D2:
                        orderHandler.HandleOrderMenu();
                        break;
                    case ConsoleKey.D3:
                        supplierHandler.HandleSupplierMenu();
                        break;
                    case ConsoleKey.D4:
                        storageHandler.HandleStorageMenu();
                        break;
                    case ConsoleKey.D5:
                        itemHandler.HandleItemMenu();
                        break;
                    case ConsoleKey.D6:
                        itemInStorageHandler.HandleItemInStorageMenu();
                        break;
                    case ConsoleKey.D7:
                        itemInOrderHandler.HandleItemInOrderMenu();
                        break;
                    case ConsoleKey.D0:
                        return; // Выход из программы
                    default:
                        Console.WriteLine("Некорректный выбор. Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}