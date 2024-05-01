using ReStoreAPI.Entities.OrderAggergate;
using System.ComponentModel.DataAnnotations;

namespace ReStoreAPI.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        [Required]
        public ShippingAddress ShippingAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderitemDto> OrderItems { get; set; }
        public long Subtotal { get; set; }
        public long DeliveryFee { get; set; }
        public string OrderStatus { get; set; }
        public long Total {  get; set; }    
        
    }
}
