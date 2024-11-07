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

        public void UpdateStorage(Storage updatedStorage)
        {
            var existingStorage = _context.Storages.Find(updatedStorage.Id);
            if (existingStorage == null)
            {
                throw new KeyNotFoundException("Storage not found.");
            }

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(updatedStorage);

            if (!Validator.TryValidateObject(updatedStorage, validationContext, validationResults, true))
            {
                throw new ValidationException($"Storage is not valid: {string.Join(", ", validationResults.Select(v => v.ErrorMessage))}");
            }

            existingStorage.Name = updatedStorage.Name;
            existingStorage.Adress = updatedStorage.Adress;
            existingStorage.PhoneNumber = updatedStorage.PhoneNumber;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while updating the storage.", ex);
            }
        }

        public void RemoveStorage(Storage storage)
        {
            var existingstorage = _context.Storages.Find(storage.Id);

            if (existingstorage == null)
            {
                throw new KeyNotFoundException("Storage not found.");
            }

            _context.Storages.Remove(storage);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while deleting the storage.", ex);
            }
        }

        public List<Storage> GetStorageByID(int id)
        {
            var storages = _context.Storages.Where(s => s.Id == id).ToList();
            return storages;
        }

        public List<Storage> GetStorageByName(string name)
        {
            var storages = _context.Storages.Where(s => s.Name!.Contains(name)).ToList();
            return storages;
        }
    }
}