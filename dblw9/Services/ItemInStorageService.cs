using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using dblw9.Models;

namespace dblw9.Services
{
    public class ItemInStorageService
    {
        private readonly MyDbContext _context;

        public ItemInStorageService(MyDbContext context)
        {
            _context = context;
        }
        public void AddItemToStorage(ItemInStorage item)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(item);

            if (!Validator.TryValidateObject(item, validationContext, validationResults, true))
            {
                throw new ValidationException($"Item is not valid: {string.Join(", ", validationResults.Select(v => v.ErrorMessage))}");
            }

            _context.ItemsInStorage.Add(item);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while saving the item to storage.", ex);
            }
        }

        // Метод для редактирования существующего элемента в хранилище
        public void UpdateItemInStorage(ItemInStorage updatedItem)
        {
            var existingItem = _context.ItemsInStorage.Find(updatedItem.Id);
            if (existingItem == null)
            {
                throw new KeyNotFoundException("Item not found in storage.");
            }

            existingItem.StorageId = updatedItem.StorageId;
            existingItem.ItemId = updatedItem.ItemId;
            existingItem.ArrialDate = updatedItem.ArrialDate;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while updating the item in storage.", ex);
            }
        }

        // Метод для удаления элемента из хранилища
        public void RemoveItemFromStorage(ItemInStorage item)
        {
            var existingItem = _context.ItemsInStorage.Find(item.Id);

            if (existingItem == null)
            {
                throw new KeyNotFoundException("Item not found in storage.");
            }

            _context.ItemsInStorage.Remove(existingItem);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while deleting the item from storage.", ex);
            }
        }

        // Метод для получения элемента по ID
        public List<ItemInStorage> GetItemById(int id)
        {
            return _context.ItemsInStorage.Where(i => i.Id == id).ToList();
        }

        // Метод для получения всех элементов в хранилище
        public List<ItemInStorage> GetAllItemsInStorage()
        {
            return _context.ItemsInStorage.ToList();
        }

        // Метод для получения элементов по ID склада
        public List<ItemInStorage> GetItemsByStorageId(int storageId)
        {
            return _context.ItemsInStorage.Where(i => i.StorageId == storageId).ToList();
        }
    }
}