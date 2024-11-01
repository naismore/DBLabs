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
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
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