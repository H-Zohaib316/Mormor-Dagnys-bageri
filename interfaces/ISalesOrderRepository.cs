using MormorBageri.DTOs.SalesOrders;

namespace MormorBageri.interfaces;

public interface ISalesOrderRepository
{
    public Task<List<GetOrdersDto>> ListAllOrders();
    public Task<GetOrderDto> FindOrderById(int id);
    public Task<GetOrderDto> FindOrderByOrderNumber(string orderNumber);
    public Task<List<CustomerOrderDto>> FindOrderByDate(DateTime date);
    public Task<bool> AddOrder(PostSalesOrderDto order);
    public Task<bool> DeleteOrder(int id);


}
