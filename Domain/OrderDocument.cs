using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain;

public class OrderDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("order_uuid")]
    public string OrderId { get; set; }
    
    [BsonElement("psp")]
    public string Psp { get; set; }
    
    [BsonElement("products")]
    public List<ProductDocument> Products { get; set; }
    
    [BsonElement("customer")]
    public CustomerDocument Customer { get; set; }
}

public class ProductDocument
{
    [BsonElement("name")]
    public string Name { get; set; }
    
    [BsonElement("description")]
    public string Description { get; set; }
    
    [BsonElement("price")]
    public decimal Price { get; set; }
}

public class CustomerDocument
{
    [BsonElement("name")]
    public string Name { get; set; }
    
    [BsonElement("email")]
    public string Email { get; set; }
    
    [BsonElement("city")]
    public string City { get; set; }
    
    [BsonElement("street")]
    public string Street { get; set; }
    
    [BsonElement("postalcode")]
    public string Postalcode { get; set; }
    
    [BsonElement("country")]
    public string Country { get; set; }
    
}