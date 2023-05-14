namespace Domain;

public class OrderProduct
{
    public int ID { get; set; }
    public Product Product { get; set; }
    public int ProductId { get; set; }
    public Order Order { get; set; }
    public int OrderId { get; set; }
}