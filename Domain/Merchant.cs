namespace Domain;

public class Merchant
{
    public int ID { get; set; }
    public string Email { get; set; }
    public IEnumerable<Product> Products { get; set; }
}