namespace Domain;

public class Address
{
    public int ID { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Street { get; set; }
    public string Country { get; set; }
    public ICollection<Customer> Customers { get; set; }
}