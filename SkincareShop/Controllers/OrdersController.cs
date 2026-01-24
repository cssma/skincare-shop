using KoreanSkincareShop.Models;
using KoreanSkincareShop.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KoreanSkincareShop.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private IOrderRepository orderRepository;
    private IShoppingCartRepository shopCartRepository;

    public OrdersController(IOrderRepository orderRepository, IShoppingCartRepository shopCartRepository)
    {
        this.orderRepository = orderRepository;
        this.shopCartRepository = shopCartRepository;
    }
    
    public IActionResult Checkout()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Checkout(Order order)
    {
        await orderRepository.PlaceOrderAsync(order);
        shopCartRepository.ClearCart();
        HttpContext.Session.SetInt32("CartCount", 0);
        return RedirectToAction("SelectMethod", "Payment", new { orderId = order.Id });
    }

    public IActionResult CheckoutComplete()
    {
        return View();
    }
}