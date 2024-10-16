namespace dblw9
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            using (MyDbContext db = new MyDbContext())
            {
                Supplier supplier1 = new Supplier { Name = "АО ММЗ", ContactPersonFirstName = "Аяз", ContactPersonLastName = "Ахметшин", PhoneNumber = "+7123412312", Adress = "ayazAhmetshin@nAts.eu"};

                db.Add(supplier1);
                db.SaveChanges();
            }

            using (MyDbContext db = new MyDbContext())
            {
                Supplier? supplier = db.Suppliers.FirstOrDefault();

                if (supplier != null)
                {
                    Console.WriteLine($"{supplier.Id}. {supplier.Name}\nОтветственный: {supplier.ContactPerson}\nНомер телефона: {supplier.PhoneNumber}\nАдрес: {supplier.Adress}");
                }
            }
        }
    }
}
