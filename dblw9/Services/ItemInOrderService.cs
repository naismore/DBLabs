using dblw9.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace dblw9.Services
{
    public class ItemInOrderService
    {
        private readonly MyDbContext _context;


        public ItemInOrderService(MyDbContext context)

        {

            _context = context;

        }


        public List<ItemInOrder> GetAllItemsInOrder()

        {

            return _context.ItemsInOrder.Include(io => io.Item).Include(io => io.Order).ToList();

        }


        public void AddItemInOrder(ItemInOrder itemInOrder)

        {

            var validationResults = new List<ValidationResult>();

            var validationContext = new ValidationContext(itemInOrder);


            if (!Validator.TryValidateObject(itemInOrder, validationContext, validationResults, true))

            {

                throw new ValidationException($"ItemInOrder is not valid: {string.Join(", ", validationResults.Select(v => v.ErrorMessage))}");

            }


            _context.ItemsInOrder.Add(itemInOrder);


            try

            {

                _context.SaveChanges();

            }

            catch (DbUpdateException ex)

            {

                throw new Exception("An error occurred while saving the item in order.", ex);

            }

        }


        public void UpdateItemInOrder(ItemInOrder updatedItemInOrder)

        {

            var existingItemInOrder = _context.ItemsInOrder.Find(updatedItemInOrder.Id);

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


        public void RemoveItemInOrder(ItemInOrder itemInOrder)

        {

            var existingItemInOrder = _context.ItemsInOrder.Find(itemInOrder.Id);

            if (existingItemInOrder == null)

            {

                throw new KeyNotFoundException("ItemInOrder not found.");

            }

            _context.ItemsInOrder.Remove(existingItemInOrder);


            try

            {

                _context.SaveChanges();

            }

            catch (DbUpdateException ex)

            {

                throw new Exception("An error occurred while saving the item in order.", ex);

            }

        }


        public List<ItemInOrder> GetItemsInOrderByOrderId(int orderId)

        {

            return _context.ItemsInOrder.Where(io => io.OrderId == orderId).Include(io => io.Item).ToList();

        }
    }
}
