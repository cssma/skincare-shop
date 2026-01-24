using KoreanSkincareShop.Data;
using KoreanSkincareShop.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KoreanSkincareShop.Models.Services;

public class OrderRepository : IOrderRepository
{
    private readonly SkincareShopDbContext _dbContext;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly TimeProvider _timeProvider;

    public OrderRepository(
        SkincareShopDbContext dbContext,
        IShoppingCartRepository shoppingCartRepository,
        TimeProvider timeProvider)
    {
        _dbContext = dbContext;
        _shoppingCartRepository = shoppingCartRepository;
        _timeProvider = timeProvider;
    }

    public async Task<Order> PlaceOrderAsync(Order order)
    {
        var shoppingCartItems = _shoppingCartRepository.GetShoppingCartItems();

        if (shoppingCartItems.Count == 0)
        {
            throw new InvalidOperationException("Cannot place order with empty cart");
        }

        order.OrderDetails = shoppingCartItems
            .Where(item => item.Product != null)
            .Select(item => new OrderDetail
            {
                Quantity = item.Qty,
                ProductId = item.Product!.Id,
                Price = item.Product.Price
            })
            .ToList();

        order.OrderPlaced = _timeProvider.GetLocalNow().DateTime;
        order.OrderTotal = _shoppingCartRepository.GetShoppingCartTotal();

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        return order;
    }

    public async Task<Order?> GetOrderByIdAsync(int orderId)
    {
        return await _dbContext.Orders
            .Include(o => o.OrderDetails)!
            .ThenInclude(od => od.Product)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }
}
