using dblw9.Models;
using dblw9.Services;
using System.ComponentModel.DataAnnotations;

namespace dblw9.Handlers
{
    public class SupplierHandler
    {
        private readonly SupplierService _supplierService;

        public SupplierHandler(SupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public void HandleSupplierMenu()
        {
            Console.Clear();
            Menu.ShowSupplierMenu();
            var key1 = Console.ReadKey(true).Key;

            switch (key1)
            {
                case ConsoleKey.D1:
                    WriteAllSuppliers();
                    Console.ReadKey();
                    break;
                case ConsoleKey.D2:
                    AddSupplier();
                    break;
                case ConsoleKey.D3:
                    EditSupplier();
                    break;
                case ConsoleKey.D4:
                    SearchSuppliers(); // Добавляем вызов метода поиска
                    break;
                case ConsoleKey.D5:
                    RemoveSupplier(); // Добавляем вызов метода удаления
                    break;
            }
        }

        private void WriteAllSuppliers()
        {
            Console.Clear();
            var suppliers = _supplierService.GetAllSuppliers();
            if (suppliers != null && suppliers.Any())
            {
                Console.WriteLine("Поставщики: ");
                foreach (var supplier in suppliers)
                {
                    Console.WriteLine($"{supplier.Id}. {supplier.Name}\n");
                }
            }
            else
            {
                Console.WriteLine("Поставщики не найдены");
            }
        }

        private void AddSupplier()
        {
            var newSupplier = new Supplier();

            Console.WriteLine("Введите данные для нового поставщика:");

            Console.Write("Название: ");
            newSupplier.Name = Console.ReadLine();

            try
            {
                _supplierService.AddSupplier(newSupplier);
                Console.WriteLine("Поставщик успешно добавлен.");
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

        private void EditSupplier()
        {
            Console.Write("Введите ID поставщика для редактирования: ");
            if (int.TryParse(Console.ReadLine(), out int supplierId))
            {
                var supplier = _supplierService.GetSupplierByID(supplierId).FirstOrDefault();
                if (supplier != null)
                {
                    Console.WriteLine($"Текущие данные о поставщике: Название - {supplier.Name}");

                    Console.Write("Новое название (оставьте пустым для сохранения текущего): ");
                    var newName = Console.ReadLine();
                    supplier.Name = string.IsNullOrWhiteSpace(newName) ? supplier.Name : newName;

                    try
                    {
                        _supplierService.UpdateSupplier(supplier);
                        Console.WriteLine("Поставщик успешно обновлен.");
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
                    Console.WriteLine("Поставщик с таким ID не найден.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод ID.");
            }
        }

        private void SearchSuppliers()
        {
            Console.Write("Введите название поставщика для поиска: ");
            var searchTerm = Console.ReadLine();

            var foundSuppliers = _supplierService.GetSupplierByName(searchTerm);
            if (foundSuppliers != null && foundSuppliers.Any())
            {
                Console.WriteLine("Найденные поставщики: ");
                foreach (var supplier in foundSuppliers)
                {
                    Console.WriteLine($"{supplier.Id}. {supplier.Name}\n");
                }
            }
            else
            {
                Console.WriteLine("Поставщики не найдены.");
            }
        }

        private void RemoveSupplier()
        {
            Console.Write("Введите ID поставщика для удаления: ");

            if (int.TryParse(Console.ReadLine(), out int supplierId))
            {
                var supplier = _supplierService.GetSupplierByID(supplierId).FirstOrDefault();
                if (supplier != null)
                {
                    try
                    {
                        _supplierService.RemoveSupplier(supplier);
                        Console.WriteLine("Поставщик успешно удален.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Поставщик с таким ID не найден.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод ID.");
            }
        }
    }
}