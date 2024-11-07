using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(storage);

            if (!Validator.TryValidateObject(storage, validationContext, validationResults, true))
            {
                throw new ValidationException($"Storage is not valid: {string.Join(", ", validationResults.Select(v => v.ErrorMessage))}");
            }

            _context.Storages.Add(storage);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while saving the storage.", ex);
            }
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