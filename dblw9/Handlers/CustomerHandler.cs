using dblw9.Models;
using dblw9.Services;
using System.ComponentModel.DataAnnotations;

namespace dblw9.Handlers
{
    public class CustomerHandler
    {
        private readonly CustomerService _customerService;

        public CustomerHandler(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public void HandleCustomerMenu()
        {
            Console.Clear();
            Menu.ShowCustomerMenu();
            var key1 = Console.ReadKey(true).Key;

            switch (key1)
            {
                case ConsoleKey.D1:
                    WriteAllCustomers();
                    Console.ReadKey();
                    break;
                case ConsoleKey.D2:
                    AddCustomer();
                    break;
                case ConsoleKey.D3:
                    EditCustomer();
                    break;
                case ConsoleKey.D4:
                    SearchCustomers(); // Добавляем вызов метода поиска
                    break;
                case ConsoleKey.D5:
                    RemoveCustomer(); // Добавляем вызов метода удаления
                    break;
            }
        }

        private void WriteAllCustomers()
        {
            Console.Clear();
            var customers = _customerService.GetAllCustomers();
            if (customers != null && customers.Any())
            {
                Console.WriteLine("Клиенты: ");
                foreach (var customer in customers)
                {
                    Console.WriteLine($"{customer.Id}. {customer.FirstName} {customer.LastName}\n\tEmail: {customer.Email}\n\tДата рождения: {customer.BirthDate}\n");
                }
            }
            else
            {
                Console.WriteLine("Клиенты не найдены");
            }
        }

        private void AddCustomer()
        {
            var newCustomer = new Customer();

            Console.WriteLine("Введите данные для нового клиента:");

            Console.Write("Имя: ");
            newCustomer.FirstName = Console.ReadLine();

            Console.Write("Фамилия: ");
            newCustomer.LastName = Console.ReadLine();

            Console.Write("Email: ");
            newCustomer.Email = Console.ReadLine();

            Console.Write("Дата рождения (yyyy-mm-dd): ");
            newCustomer.BirthDate = DateTime.Parse(Console.ReadLine());

            try
            {
                _customerService.AddCustomer(newCustomer);
                Console.WriteLine("Клиент успешно добавлен.");
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

        private void EditCustomer()
        {
            Console.Write("Введите ID клиента для редактирования: ");
            if (int.TryParse(Console.ReadLine(), out int customerId))
            {
                var customer = _customerService.GetCustomerByID(customerId).FirstOrDefault();
                if (customer != null)
                {
                    Console.WriteLine($"Текущие данные о клиенте: Имя - {customer.FirstName}, Фамилия - {customer.LastName}, Email - {customer.Email}, Дата рождения - {customer.BirthDate}");

                    Console.Write("Новое имя (оставьте пустым для сохранения текущего): ");
                    var newFirstName = Console.ReadLine();
                    customer.FirstName = string.IsNullOrWhiteSpace(newFirstName) ? customer.FirstName : newFirstName;

                    Console.Write("Новая фамилия (оставьте пустым для сохранения текущего): ");
                    var newLastName = Console.ReadLine();
                    customer.LastName = string.IsNullOrWhiteSpace(newLastName) ? customer.LastName : newLastName;

                    Console.Write("Новый Email (оставьте пустым для сохранения текущего): ");
                    var newEmail = Console.ReadLine();
                    customer.Email = string.IsNullOrWhiteSpace(newEmail) ? customer.Email : newEmail;

                    Console.Write("Новая дата рождения (оставьте пустым для сохранения текущей): ");
                    var birthDateInput = Console.ReadLine();
                    customer.BirthDate = string.IsNullOrWhiteSpace(birthDateInput) ? customer.BirthDate : DateTime.Parse(birthDateInput);

                    try
                    {
                        _customerService.UpdateCustomer(customer);
                        Console.WriteLine("Клиент успешно обновлен.");
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
                    Console.WriteLine("Клиент с таким ID не найден.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод ID.");
            }
        }

        private void SearchCustomers()
        {
            Console.Write("Введите имя или фамилию клиента для поиска: ");
            var searchTerm = Console.ReadLine();

            var foundCustomers = _customerService.GetCustomerByName(searchTerm);
            if (foundCustomers != null && foundCustomers.Any())
            {
                Console.WriteLine("Найденные клиенты: ");
                foreach (var customer in foundCustomers)
                {
                    Console.WriteLine($"{customer.Id}. {customer.FirstName} {customer.LastName}\n\tEmail: {customer.Email}\n\tДата рождения: {customer.BirthDate}\n");
                }
            }
            else
            {
                Console.WriteLine("Клиенты не найдены.");
            }
        }

        private void RemoveCustomer()
        {
            Console.Write("Введите ID клиента для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int customerId))
            {
                var customer = _customerService.GetCustomerByID(customerId).FirstOrDefault();
                if (customer != null)
                {
                    try
                    {
                        _customerService.RemoveCustomer(customer);
                        Console.WriteLine("Клиент успешно удален.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Клиент с таким ID не найден.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод ID.");
            }
        }
    }
}