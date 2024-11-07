using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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

        public void UpdateItem(Item item)
        {
            _context.Items.Update(item);
            _context.SaveChanges();
        }

        public void RemoveItem(Item item)
        {
            _context.Items.Remove(item);
            _context.SaveChanges();
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