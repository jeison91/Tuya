namespace Tuya.Domain.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public int IdCustomer { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public required string Address { get; set; }
        public string State { get; set; } = string.Empty;
        public decimal Total { get; set; }

        public CustomerEntity Customer { get; set; } = null!;
        public ICollection<OrderDetailEntity> OrderDetails { get; set; } = [];
    }
}
