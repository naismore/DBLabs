using Microsoft.EntityFrameworkCore;

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
            return _context.Items.AsNoTracking().ToList();
        }

        public void AddItem(Item item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();
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