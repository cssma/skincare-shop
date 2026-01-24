using KoreanSkincareShop.Models;
using KoreanSkincareShop.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KoreanSkincareShop.Controllers;

[Authorize]
public class PaymentController : Controller
{
    private readonly IPaymentService _paymentService;
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(
        IPaymentService paymentService,
        IOrderRepository orderRepository,
        ILogger<PaymentController> logger)
    {
        _paymentService = paymentService;
        _orderRepository = orderRepository;
        _logger = logger;
    }

    // =======================
    // SELECT PAYMENT METHOD
    // =======================
    [HttpGet]
    public IActionResult SelectMethod(int orderId)
    {
        if (orderId <= 0)
            return RedirectToAction("Checkout", "Orders");

        ViewBag.OrderId = orderId;
        return View();
    }

    // =======================
    // INIT PAYMENT
    // =======================
    [HttpPost]
    public async Task<IActionResult> Process(int orderId, PaymentMethod paymentMethod)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);

        if (order == null)
            return RedirectToAction("Checkout", "Orders");

        var payment = await _paymentService.InitiatePaymentAsync(order, paymentMethod);

        if (payment == null || payment.Id <= 0)
        {
            TempData["ErrorMessage"] = "Payment initialization failed.";
            return RedirectToAction("SelectMethod", new { orderId });
        }

        _logger.LogInformation(
            "Payment initialized. PaymentId={PaymentId}, OrderId={OrderId}",
            payment.Id, orderId);

        return RedirectToAction("Processing", new { paymentId = payment.Id });
    }

    // =======================
    // PROCESS PAYMENT (BACKEND)
    // =======================
    [HttpGet]
    public async Task<IActionResult> Processing(int paymentId)
    {
        if (paymentId <= 0)
            return RedirectToAction("Failed", new { paymentId });

        var payment = await _paymentService.GetPaymentByIdAsync(paymentId);

        if (payment == null)
            return RedirectToAction("Failed", new { paymentId });

        await _paymentService.ProcessPaymentAsync(paymentId);

        payment = await _paymentService.GetPaymentByIdAsync(paymentId);

        if (payment!.Status == PaymentStatus.Completed)
            return RedirectToAction("Success", new { paymentId });

        return RedirectToAction("Failed", new { paymentId });
    }

    // =======================
    // SUCCESS
    // =======================
    [HttpGet]
    public async Task<IActionResult> Success(int paymentId)
    {
        var payment = await _paymentService.GetPaymentByIdAsync(paymentId);

        if (payment == null || payment.Status != PaymentStatus.Completed)
            return RedirectToAction("Failed", new { paymentId });

        return View(payment);
    }

    // =======================
    // FAILED
    // =======================
    [HttpGet]
    public async Task<IActionResult> Failed(int paymentId)
    {
        var payment = await _paymentService.GetPaymentByIdAsync(paymentId);

        if (payment == null)
            return RedirectToAction("Checkout", "Orders");

        return View(payment);
    }
}
