namespace Order.API.Models
{
    public class DtoOrder
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public List<DtoOrderItem> OrderItems { get; set; }
    }
}
