using dblw9.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace dblw9.Services
{
    public class CustomerService
    {
        private readonly MyDbContext _context;


        public CustomerService(MyDbContext context)

        {

            _context = context;

        }


        public List<Customer> GetAllCustomers()

        {

            return _context.Customers.ToList();

        }


        public void AddCustomer(Customer customer)

        {

            var validationResults = new List<ValidationResult>();

            var validationContext = new ValidationContext(customer);


            if (!Validator.TryValidateObject(customer, validationContext, validationResults, true))

            {

                throw new ValidationException($"Customer is not valid: {string.Join(", ", validationResults.Select(v => v.ErrorMessage))}");

            }


            _context.Customers.Add(customer);


            try

            {

                _context.SaveChanges();

            }

            catch (DbUpdateException ex)

            {

                throw new Exception("An error occurred while saving the customer.", ex);

            }

        }


        public void UpdateCustomer(Customer updatedCustomer)

        {

            var existingCustomer = _context.Customers.Find(updatedCustomer.Id);

            if (existingCustomer == null)

            {

                throw new KeyNotFoundException("Customer not found.");

            }


            var validationResults = new List<ValidationResult>();

            var validationContext = new ValidationContext(updatedCustomer);


            if (!Validator.TryValidateObject(updatedCustomer, validationContext, validationResults, true))

            {

                throw new ValidationException($"Customer is not valid: {string.Join(", ", validationResults.Select(v => v.ErrorMessage))}");

            }


            existingCustomer.FirstName = updatedCustomer.FirstName;

            existingCustomer.LastName = updatedCustomer.LastName;

            existingCustomer.BirthDate = updatedCustomer.BirthDate;

            existingCustomer.Email = updatedCustomer.Email;


            try

            {

                _context.SaveChanges();

            }

            catch (DbUpdateException ex)

            {

                throw new Exception("An error occurred while updating the customer.", ex);

            }

        }


        public void RemoveCustomer(Customer customer)

        {

            var existingCustomer = _context.Customers.Find(customer.Id);

            if (existingCustomer == null)

            {

                throw new KeyNotFoundException("Customer not found.");

            }

            _context.Customers.Remove(existingCustomer);


            try

            {

                _context.SaveChanges();

            }

            catch (DbUpdateException ex)

            {

                throw new Exception("An error occurred while saving the customer.", ex);

            }

        }


        public List<Customer> GetCustomerByID(int id)

        {

            return _context.Customers.Where(c => c.Id == id).ToList();

        }


        public List<Customer> GetCustomerByName(string name)

        {

            return _context.Customers.Where(c => c.FirstName!.Contains(name) || c.LastName!.Contains(name)).ToList();

        }
    }
}
