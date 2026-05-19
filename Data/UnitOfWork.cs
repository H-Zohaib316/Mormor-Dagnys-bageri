using MormorBageri.interfaces;
using MormorBageri.repositories;

namespace MormorBageri.Data;

public class UnitOfWork(EShopContext context) : IUnitOfWork
{
    public IProductRepository ProductRepository => new ProductRepository(context);

    public ICustomerRepository CustomerRepository =>  new CustomerRepository(context);

    public ISalesOrderRepository SalesOrderRepository =>  new SalesOrderRepository(context);

    public async Task<bool> Complete()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
