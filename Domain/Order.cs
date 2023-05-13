namespace Domain;

public class Order
{
    public int ID { get; set; }
    public Guid OrderId { get; set; }
    public PSP Psp { get; set; }
    public Customer Customer { get; set; }
    public int CustomerId { get; set; }
    public ICollection<OrderProduct> OrderProducts { get; set; }
}