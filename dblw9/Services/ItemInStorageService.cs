namespace dblw9.Services
{
    public class ItemInStorageService
    {
        private readonly MyDbContext _context;

        public ItemInStorageService(MyDbContext context)
        {
            _context = context;
        }

        public void Update(ItemsInStorage iis)
        {
            _context.ItemsInStorages.Update(iis);
            _context.SaveChanges();
        }
    }
}