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
    public class ItemsInOrderService
    {
        private readonly MyDbContext _context;


        public ItemsInOrderService(MyDbContext context)

        {

            _context = context;

        }


        public List<ItemsInOrder> GetAllItemsInOrder()

        {

            return _context.ItemsInOrders.Include(io => io.Item).Include(io => io.Order).ToList();

        }


        public void AddItemInOrder(ItemsInOrder itemInOrder)

        {

            var validationResults = new List<ValidationResult>();

            var validationContext = new ValidationContext(itemInOrder);


            if (!Validator.TryValidateObject(itemInOrder, validationContext, validationResults, true))

            {

                throw new ValidationException($"ItemInOrder is not valid: {string.Join(", ", validationResults.Select(v => v.ErrorMessage))}");

            }


            _context.ItemsInOrders.Add(itemInOrder);


            try

            {

                _context.SaveChanges();

            }

            catch (DbUpdateException ex)

            {

                throw new Exception("An error occurred while saving the item in order.", ex);

            }

        }


        public void UpdateItemInOrder(ItemsInOrder updatedItemInOrder)

        {

            var existingItemInOrder = _context.ItemsInOrders.Find(updatedItemInOrder.Id);

            if (existingItemInOrder == null)

            {

                throw new KeyNotFoundException("ItemInOrder not found.");

            }


            var validationResults = new List<ValidationResult>();

            var validationContext = new ValidationContext(updatedItemInOrder);


            if (!Validator.TryValidateObject(updatedItemInOrder, validationContext, validationResults, true))

            {

                throw new ValidationException($"ItemInOrder is not valid: {string.Join(", ", validationResults.Select(v => v.ErrorMessage))}");

            }


            existingItemInOrder.ItemId = updatedItemInOrder.ItemId;

            existingItemInOrder.OrderId = updatedItemInOrder.OrderId;

            existingItemInOrder.Quantity = updatedItemInOrder.Quantity;


            try

            {

                _context.SaveChanges();

            }

            catch (DbUpdateException ex)

            {

                throw new Exception("An error occurred while updating the item in order.", ex);

            }

        }


        public void RemoveItemInOrder(ItemsInOrder itemInOrder)

        {

            var existingItemInOrder = _context.ItemsInOrders.Find(itemInOrder.Id);

            if (existingItemInOrder == null)

            {

                throw new KeyNotFoundException("ItemInOrder not found.");

            }

            _context.ItemsInOrders.Remove(existingItemInOrder);


            try

            {

                _context.SaveChanges();

            }

            catch (DbUpdateException ex)

            {

                throw new Exception("An error occurred while saving the item in order.", ex);

            }

        }


        public List<ItemsInOrder> GetItemsInOrderByOrderId(int orderId)

        {

            return _context.ItemsInOrders.Where(io => io.OrderId == orderId).Include(io => io.Item).ToList();

        }
    }
}
