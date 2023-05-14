namespace Domain;

public class Customer
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Address Address { get; set; }
    public int AddressId { get; set; }
    public ICollection<Order> Orders { get; set; }
}