using Microsoft.EntityFrameworkCore;
using ReStoreAPI.DTOs;
using ReStoreAPI.Entities;

namespace ReStoreAPI.Extension
{
    public  static class BasketExtension
    {
        public static BasketDTo MapBasketToDto(this Basket basket)
        {
            return new BasketDTo
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    ProductId = item.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    PictureUrl = item.Product.PictureUrl,
                    Type = item.Product.Type,
                    Brand = item.Product.Brand,
                    Quantity = item.Quantity

                }).ToList()

            };
        }
        public static IQueryable<Basket> RetrieveBasketWithItems(this IQueryable<Basket> query,string BuyerId)
        {
            return query.Include(i => i.Items).ThenInclude(p=>p.Product).Where(b=> b.BuyerId == BuyerId);
            
        }
    }
}
