namespace MormorBageri.interfaces;

public interface IUnitOfWork
{
    IProductRepository ProductRepository{ get; }
    ICustomerRepository CustomerRepository{ get; }
    ISalesOrderRepository SalesOrderRepository{ get; }
    Task<bool> Complete();

}
