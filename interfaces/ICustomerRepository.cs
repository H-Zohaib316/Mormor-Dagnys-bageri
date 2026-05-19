using MormorBageri.DTOs.Customers;

namespace MormorBageri.interfaces;

public interface ICustomerRepository
{
    public Task<List<GetAllCustomersDto>> ListAllCustomers();
    public Task<GetCustomerDto> FindCustomer(int id);
    public Task<bool> AddCustomer(PostCustomerDto customer);
    public Task<bool> UpdateCustomerContactPerson(int id, PatchCustomerContactDto contactPerson);
    public Task<List<GetCustomersProductsDto>> ListCustomerProducts();
}
