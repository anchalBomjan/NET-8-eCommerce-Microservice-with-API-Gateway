﻿using eCommerce.SharedLibrary.Logs;
using eCommerce.SharedLibrary.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderApi.Application.Interfaces;
using OrderApi.Domain.Entities;
using OrderApi.Infrastructure.Data;
using System.Linq.Expressions;


namespace OrderApi.Infrastructure.Repositories
{
    public class OrderRepository(OrderDbContext context) : IOrder
    {
        public  async Task<Response> CreateAsync(Order entity)
        {
            try
            {
                var order = context.Orders.Add(entity).Entity;
                await context.SaveChangesAsync();
                return order.Id > 0 ? new Response(true, "Order placed successfully") :
                    new Response(false, "Errors occoured while placing order");

            }
            catch(Exception ex)
            {
                // Log Original Exception
                LogException.LogExceptions(ex);
                //Display Scary-free Message to client

                return new Response(false, "Error occured  while placing order");
            }
        }

        public async  Task<Response> DeleteAsync(Order entity)
        {
            try
            {
                var order = await FindByIdAsync(entity.Id);
                if (order is null)
                    return new Response(false, "Order not found");
                context.Orders.Remove(entity);
                await context.SaveChangesAsync();
                return new Response(true, " Order successfullt delete");


            }
            catch (Exception ex)
            {
                // Log Original Exception
                LogException.LogExceptions(ex);
                //Display Scary-free Message to client

                return new Response(false, "Error occured  while placing order");
            }
        }

        public  async Task<Order> FindByIdAsync(int id)
        {
            try
            {
                var order = await context.Orders!.FindAsync(id);

                return order is not null ? order : null!;

            }
            catch (Exception ex)
            {
                // Log Original Exception
                LogException.LogExceptions(ex);
                //Display Scary-free Message to client

                 throw new Exception ( "Error occured  while placing order");
            }
        }

        public  async Task<IEnumerable<Order>> GetAllAsync()
        {
            try
            {
                 var orders= await  context.Orders.AsNoTracking().ToListAsync();
                return orders is not null ? orders : null!;

            }
            catch (Exception ex)
            {
                // Log Original Exception
                LogException.LogExceptions(ex);
                //Display Scary-free Message to client

               throw new Exception ( "Error occured  while placing order");
            }
        }

        public async Task<Order> GetByAsync(Expression<Func<Order, bool>> predicate)
        {
            try
            {
                var order = await context.Orders.Where(predicate).FirstOrDefaultAsync();
                return order is not null ? order : null!;

            }
            catch (Exception ex)
            {
                // Log Original Exception
                LogException.LogExceptions(ex);
                //Display Scary-free Message to client

                throw new Exception("Error occured  while retrieving  order");
            }
        }

        public Task<IEnumerable<Order>> GetOrdersAsync(Expression<Func<Order, bool>> predicate)
        {
            try
            {

            }
            catch (Exception ex)
            {
                // Log Original Exception
                LogException.LogExceptions(ex);
                //Display Scary-free Message to client

                return new Response(false, "Error occured  while placing order");
            }
        }

        public Task<Response> UpdateAsync(Order entity)
        {
            try
            {

            }
            catch (Exception ex)
            {
                // Log Original Exception
                LogException.LogExceptions(ex);
                //Display Scary-free Message to client

                return new Response(false, "Error occured  while placing order");
            }
        }
    }
}
