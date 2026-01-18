using KoreanSkincareShop.Data;
using KoreanSkincareShop.Models.Interfaces;

namespace KoreanSkincareShop.Models.Services;

public class OrderRepository : IOrderRepository
{
    private SkincareShopDbContext dbContext;
    private IShoppingCartRepository shoppingCartRepository;

    public OrderRepository(SkincareShopDbContext dbContext, IShoppingCartRepository shoppingCartRepository)
    {
        this.dbContext = dbContext;
        this.shoppingCartRepository = shoppingCartRepository;
    }
    
    public void PlaceOrder(Order order)
    {
        var shoppingCartItems = shoppingCartRepository.GetShoppingCartItems();
        order.OrderDetails = new List<OrderDetail>();
        foreach (var item in shoppingCartItems)
        {
            var orderDetail = new OrderDetail
            {
                Quantity = item.Qty,
                ProductId = item.Product.Id,
                Price = item.Product.Price
            };
            order.OrderDetails.Add(orderDetail);
        }
        order.OrderPlaced = DateTime.Now;
        order.OrderTotal = shoppingCartRepository.GetShoppingCartTotal();
        dbContext.Orders.Add(order);
        dbContext.SaveChanges();
    }
}