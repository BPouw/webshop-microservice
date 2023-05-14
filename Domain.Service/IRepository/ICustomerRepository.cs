namespace Domain.Service.IRepository;

public interface ICustomerRepository
{
    Task<Customer> getCustomerById(int id);
}