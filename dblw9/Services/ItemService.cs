using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using dblw9.Models;

namespace dblw9.Services
{
    public class ItemService
    {
        private readonly MyDbContext _context;

        public ItemService(MyDbContext context)
        {
            _context = context;
        }

        public List<Item> GetAllItems()
        {
            return _context.Items.ToList();
        }


        public void AddItem(Item item)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(item);

            if (!Validator.TryValidateObject(item, validationContext, validationResults, true))
            {
                throw new ValidationException($"Item is not valid: {string.Join(", ", validationResults.Select(v => v.ErrorMessage))}");
            }

            _context.Items.Add(item);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while saving the item.", ex);
            }
        }

        public void UpdateItem(Item updatedItem)
        {
            // ѕроверка на существование товара
            var existingItem = _context.Items.Find(updatedItem.Id);
            if (existingItem == null)
            {
                throw new KeyNotFoundException("Item not found.");
            }

            // ¬алидаци€ обновленного товара
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(updatedItem);

            if (!Validator.TryValidateObject(updatedItem, validationContext, validationResults, true))
            {
                throw new ValidationException($"Item is not valid: {string.Join(", ", validationResults.Select(v => v.ErrorMessage))}");
            }

            // ќбновление свойств существующего товара
            existingItem.Name = updatedItem.Name;
            existingItem.Description = updatedItem.Description;
            existingItem.Cost = updatedItem.Cost;
            existingItem.Unit = updatedItem.Unit;
            existingItem.SupplierId = updatedItem.SupplierId;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while updating the item.", ex);
            }
        }

        public void RemoveItem(Item item)
        {
            var existingItem = _context.Items.Find(item.Id);
            if (existingItem == null)
            {
                throw new KeyNotFoundException("Item not found.");
            }
            _context.Items.Remove(item);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while saving the item.", ex);
            }
        }

        public List<Item> GetItemByID(int id)
        {
            var items = _context.Items.Where(i => i.Id == id).ToList();
            return items;
        }

        public List<Item> GetItemByName(string name)
        {
            var items = _context.Items.Where(i => i.Name!.Contains(name)).ToList();
            return items;
        }
    }
}