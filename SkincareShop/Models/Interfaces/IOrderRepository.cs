namespace KoreanSkincareShop.Models.Interfaces;

public interface IOrderRepository
{
    Task<Order> PlaceOrderAsync(Order order);
    Task<Order?> GetOrderByIdAsync(int orderId);
}