using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReStore.Controllers;
using ReStore.Data;

using ReStoreAPI.DTOs;
using ReStoreAPI.Entities;
using ReStoreAPI.Extension;

namespace ReStoreAPI.Controllers
{
    public class BasketController : BaseApiController

    {
        private readonly StoreContext _context;
        public BasketController (StoreContext context)
        {
            _context = context;
        }
        [HttpGet (Name = "GetBasket")]
        public async Task<ActionResult<BasketDTo>> GetBasket()
        {
            Basket basket = await RetrieveBasket(GetBuyerId());

            if (basket == null)
                return NotFound();
            return basket.MapBasketToDto();
        }

       


        [HttpPost]
        public async Task<ActionResult<BasketDTo>> AddItemToBasket(int productId, int quantity)
        {
            var basket = await RetrieveBasket(GetBuyerId());

            if (basket == null) basket = CreateBasket();

            var product = await _context.Products.FindAsync(productId);

            if (product == null) return BadRequest(new ProblemDetails { Title = "product not found" });

            basket.AddItem(product, quantity);


            var result = await _context.SaveChangesAsync()>0;


            if (result) return CreatedAtRoute("GetBasket", basket.MapBasketToDto());

            return BadRequest(new ProblemDetails {Title = "Problem saving item to basket" });

        }



        [HttpDelete]
        public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
        {
            var basket = await RetrieveBasket(GetBuyerId());
            if (basket == null) return NotFound();

            basket.RemoveItem(productId, quantity);

            var result = await _context.SaveChangesAsync() > 0;
            if (result) return Ok();


            return BadRequest(new ProblemDetails
            {
                Title = "Problem Removing Item From The Basket"
            });

        }
        private async Task<Basket> RetrieveBasket(string buyerId)
        {
            if (string.IsNullOrEmpty(buyerId))
            {
                Response.Cookies.Delete("buyerId");
                return null;
            }
            return await _context.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.BuyerId == buyerId);
        }
        private string GetBuyerId()
        {
            return User.Identity?.Name ?? Request.Cookies["buyerId"]; 
        }
        private Basket CreateBasket()
        {
            var buyerId = User.Identity?.Name;
            if(string.IsNullOrEmpty(buyerId)) { 
            buyerId=Guid.NewGuid().ToString();  
            var cookieOption = new CookieOptions {IsEssential = true,Expires = DateTime.Now.AddDays(30)};
            Response.Cookies.Append("buyerId", buyerId, cookieOption);
            }
            var basket = new Basket { BuyerId = buyerId };
            _context.Baskets.Add(basket);
            return basket;

        }


    }

}

