using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace dblw9.Services
{
    public class ItemInStorageService
    {
        private readonly MyDbContext _context;

        public ItemInStorageService(MyDbContext context)
        {
            _context = context;
        }
        public void AddItemToStorage(ItemsInStorage item)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(item);

            if (!Validator.TryValidateObject(item, validationContext, validationResults, true))
            {
                throw new ValidationException($"Item is not valid: {string.Join(", ", validationResults.Select(v => v.ErrorMessage))}");
            }

            _context.ItemsInStorages.Add(item);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while saving the item to storage.", ex);
            }
        }

        // ����� ��� �������������� ������������� �������� � ���������
        public void UpdateItemInStorage(ItemsInStorage updatedItem)
        {
            var existingItem = _context.ItemsInStorages.Find(updatedItem.Id);
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

        // ����� ��� �������� �������� �� ���������
        public void RemoveItemFromStorage(ItemsInStorage item)
        {
            var existingItem = _context.ItemsInStorages.Find(item.Id);

            if (existingItem == null)
            {
                throw new KeyNotFoundException("Item not found in storage.");
            }

            _context.ItemsInStorages.Remove(existingItem);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while deleting the item from storage.", ex);
            }
        }

        // ����� ��� ��������� �������� �� ID
        public List<ItemsInStorage> GetItemById(int id)
        {
            return _context.ItemsInStorages.Where(i => i.Id == id).ToList();
        }

        // ����� ��� ��������� ���� ��������� � ���������
        public List<ItemsInStorage> GetAllItemsInStorage()
        {
            return _context.ItemsInStorages.ToList();
        }

        // ����� ��� ��������� ��������� �� ID ������
        public List<ItemsInStorage> GetItemsByStorageId(int storageId)
        {
            return _context.ItemsInStorages.Where(i => i.StorageId == storageId).ToList();
        }
    }
}