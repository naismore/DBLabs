using dblw9.Models;
using dblw9.Services;
using System.ComponentModel.DataAnnotations;

namespace dblw9.Handlers
{
    public class StorageHandler
    {
        private readonly StorageService _storageService;

        public StorageHandler(StorageService storageService)
        {
            _storageService = storageService;
        }

        public void HandleStorageMenu()
        {
            Console.Clear();
            Menu.ShowStorageMenu();
            var key1 = Console.ReadKey(true).Key;

            switch (key1)
            {
                case ConsoleKey.D1:
                    WriteAllStorages();
                    Console.ReadKey();
                    break;
                case ConsoleKey.D2:
                    AddStorage();
                    break;
                case ConsoleKey.D3:
                    EditStorage();
                    break;
                case ConsoleKey.D4:
                    SearchStorages(); // Добавляем вызов метода поиска
                    break;
                case ConsoleKey.D5:
                    RemoveStorage(); // Добавляем вызов метода удаления
                    break;
            }
        }

        private void WriteAllStorages()
        {
            Console.Clear();
            var storages = _storageService.GetAllStorages();
            if (storages != null && storages.Any())
            {
                Console.WriteLine("Склады: ");
                foreach (var storage in storages)
                {
                    Console.WriteLine($"{storage.Id}. {storage.Name}\n\tАдрес: {storage.Adress}\n\tТелефон: {storage.PhoneNumber}\n");
                }
            }
            else
            {
                Console.WriteLine("Склады не найдены");
            }
        }

        private void AddStorage()
        {
            var newStorage = new Storage();

            Console.WriteLine("Введите данные для нового склада:");

            Console.Write("Название склада: ");
            newStorage.Name = Console.ReadLine();

            Console.Write("Адрес склада: ");
            newStorage.Adress = Console.ReadLine();

            Console.Write("Телефон склада: ");
            newStorage.PhoneNumber = Console.ReadLine();

            try
            {
                _storageService.AddStorage(newStorage);
                Console.WriteLine("Склад успешно добавлен.");
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

        private void EditStorage()
        {
            Console.Write("Введите ID склада для редактирования: ");
            if (int.TryParse(Console.ReadLine(), out int storageId))
            {
                var storage = _storageService.GetStorageByID(storageId).FirstOrDefault();
                if (storage != null)
                {
                    Console.WriteLine($"Текущие данные о складе: Название - {storage.Name}, Адрес - {storage.Adress}, Телефон - {storage.PhoneNumber}");

                    Console.Write("Новое название склада (оставьте пустым для сохранения текущего): ");
                    var newName = Console.ReadLine();
                    storage.Name = string.IsNullOrWhiteSpace(newName) ? storage.Name : newName;

                    Console.Write("Новый адрес склада (оставьте пустым для сохранения текущего): ");
                    var newAdress = Console.ReadLine();
                    storage.Adress = string.IsNullOrWhiteSpace(newAdress) ? storage.Adress : newAdress;

                    Console.Write("Новый телефон склада (оставьте пустым для сохранения текущего): ");
                    var newPhoneNumber = Console.ReadLine();
                    storage.PhoneNumber = string.IsNullOrWhiteSpace(newPhoneNumber) ? storage.PhoneNumber : newPhoneNumber;

                    try
                    {
                        _storageService.UpdateStorage(storage);
                        Console.WriteLine("Склад успешно обновлен.");
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
                    Console.WriteLine("Склад с таким ID не найден.");
                }
            }
            else
            {
                Console.WriteLine("Некоррект ный ввод ID.");
            }
        }

        private void SearchStorages()
        {
            Console.Write("Введите название склада для поиска: ");
            var searchTerm = Console.ReadLine();

            var foundStorages = _storageService.GetStorageByName(searchTerm);
            if (foundStorages != null && foundStorages.Any())
            {
                Console.WriteLine("Найденные склады: ");
                foreach (var storage in foundStorages)
                {
                    Console.WriteLine($"{storage.Id}. {storage.Name}\n\tАдрес: {storage.Adress}\n\tТелефон: {storage.PhoneNumber}\n");
                }
            }
            else
            {
                Console.WriteLine("Склады не найдены.");
            }
        }

        private void RemoveStorage()
        {
            Console.Write("Введите ID склада для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int storageId))
            {
                var storage = _storageService.GetStorageByID(storageId).FirstOrDefault();
                if (storage != null)
                {
                    try
                    {
                        _storageService.RemoveStorage(storage);
                        Console.WriteLine("Склад успешно удален.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Склад с таким ID не найден.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод ID.");
            }
        }
    }
}