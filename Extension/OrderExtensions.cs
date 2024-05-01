﻿using Microsoft.EntityFrameworkCore;
using ReStoreAPI.DTOs;
using ReStoreAPI.Entities.OrderAggergate;

namespace ReStoreAPI.Extension
{
    public static class OrderExtensions
    {
        public static IQueryable<OrderDto> ProjectOrderToOrderDto (this IQueryable<Order> query)
        {
            return query
                .Select(order => new OrderDto
                {
                    Id = order.Id,
                    BuyerId = order.BuyerId,
                    OrderDate = order.OrderDate,
                    ShippingAddress = order.ShippingAddress,
                    DeliveryFee = order.DeliveryFee,
                    Subtotal = order.Subtotal,
                    OrderStatus = order.OrderStatus.ToString(),
                    Total = order.GetTotal(),
                    OrderItems = order.OrderItems.Select(item=> new OrderitemDto{
                        ProductId = item.ItemOrdered.ProductId,
                        Name = item.ItemOrdered.Name,   
                        PictureUrl = item.ItemOrdered.PictureUrl, 
                        Price = item.Price,
                         Quantity =item.Quantity

                    }) .ToList()
                }
                ) .AsNoTracking();
        }
    }
}
