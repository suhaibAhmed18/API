namespace ReStoreAPI.DTOs
{
    public class BasketDTo
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }

        public List <BasketItemDto> Items { get; set; }
    }
}
