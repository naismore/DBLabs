using dblw9.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dblw9.Services
{
    public class OrderService
    {
        private readonly MyDbContext _context;


        public OrderService(MyDbContext context)

        {

            _context = context;

        }


        public List<Order> GetAllOrders()

        {

            return _context.Orders.Include(o => o.Customer).ToList();

        }


        public void AddOrder(Order order)

        {

            var validationResults = new List<ValidationResult>();

            var validationContext = new ValidationContext(order);


            if (!Validator.TryValidateObject(order, validationContext, validationResults, true))

            {

                throw new ValidationException($"Order is not valid: {string.Join(", ", validationResults.Select(v => v.ErrorMessage))}");

            }


            _context.Orders.Add(order);


            try

            {

                _context.SaveChanges();

            }

            catch (DbUpdateException ex)

            {

                throw new Exception("An error occurred while saving the order.", ex);

            }

        }


        public void UpdateOrder(Order updatedOrder)

        {

            var existingOrder = _context.Orders.Find(updatedOrder.Id);

            if (existingOrder == null)

            {

                throw new KeyNotFoundException("Order not found.");

            }


            var validationResults = new List<ValidationResult>();

            var validationContext = new ValidationContext(updatedOrder);


            if (!Validator.TryValidateObject(updatedOrder, validationContext, validationResults, true))

            {

                throw new ValidationException($"Order is not valid: {string.Join(", ", validationResults.Select(v => v.ErrorMessage))}");

            }


            existingOrder.Adress = updatedOrder.Adress;

            existingOrder.OrderDate = updatedOrder.OrderDate;

            existingOrder.CustomerId = updatedOrder.CustomerId;


            try

            {

                _context.SaveChanges();

            }

            catch (DbUpdateException ex)

            {

                throw new Exception("An error occurred while updating the order.", ex);

            }

        }


        public void RemoveOrder(Order order)

        {

            var existingOrder = _context.Orders.Find(order.Id);

            if (existingOrder == null)

            {

                throw new KeyNotFoundException("Order not found.");

            }

            _context.Orders.Remove(existingOrder);


            try

            {

                _context.SaveChanges();

            }

            catch (DbUpdateException ex)

            {

                throw new Exception("An error occurred while saving the order.", ex);

            }

        }


        public List<Order> GetOrderByID(int id)

        {

            return _context.Orders.Where(o => o.Id == id).Include(o => o.Customer).ToList();

        }


        public List<Order> GetOrdersByCustomerId(int customerId)

        {

            return _context.Orders.Where(o => o.CustomerId == customerId).Include(o => o.Customer).ToList();

        }
    }
}
