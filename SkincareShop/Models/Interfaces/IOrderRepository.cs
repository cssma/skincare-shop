namespace KoreanSkincareShop.Models.Interfaces;

public interface IOrderRepository
{
    void PlaceOrder(Order order);
    
}