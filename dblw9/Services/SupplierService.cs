using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace dblw9.Services
{
    public class SupplierService
    {
        private readonly MyDbContext _context;

        public SupplierService(MyDbContext context)
        {
            _context = context;
        }
        
        public List<Supplier> GetAllSuppliers()
        {
            return _context.Suppliers.ToList();
        }

        public void AddSupplier(Supplier supplier)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(supplier);

            if (!Validator.TryValidateObject(supplier, validationContext, validationResults, true))
            {
                throw new ValidationException($"Supplier is not valid: {string.Join(", ", validationResults.Select(v => v.ErrorMessage))}");
            }

            _context.Suppliers.Add(supplier);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while saving the supplier.", ex);
            }
        }

        public void UpdateSupplier(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            _context.SaveChanges();
        }

        public void RemoveSupplier(Supplier supplier)
        {
            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
        }

        public List<Supplier> GetSupplierByID(int id)
        {
            var suppliers = _context.Suppliers.Where(s => s.Id == id).ToList();
            return suppliers;
        }

        public List<Supplier> GetSupplierByName(string name)
        {
            var suppliers = _context.Suppliers.Where(s => s.Name!.Contains(name)).ToList();
            return suppliers;
        }
    }
}