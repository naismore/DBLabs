namespace dblw9.Services
{
    public class StorageService
    {
        private readonly MyDbContext _context;

        public StorageService(MyDbContext context)
        {
            _context = context;
        }

        public List<Storage> GetAllStorages()
        {
            return _context.Storages.ToList();
        }

        public void AddStorage(Storage storage)
        {
            _context.Storages.Add(storage);
            _context.SaveChanges();
        }

        public void UpdateStorage(Storage storage)
        {
            _context.Storages.Update(storage);
            _context.SaveChanges();
        }

        public void RemoveStorage(Storage storage)
        {
            _context.Storages.Remove(storage);
            _context.SaveChanges();
        }

        public List<Storage> GetStorageByID(int id)
        {
            var storages = _context.Storages.Where(s => s.Id == id).ToList();
            return storages;
        }

        public List<Storage> GetItemByName(string name)
        {
            var storages = _context.Storages.Where(s => s.Name!.Contains(name)).ToList();
            return storages;
        }
    }
}