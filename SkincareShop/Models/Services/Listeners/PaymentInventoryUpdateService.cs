using KoreanSkincareShop.Events;
using KoreanSkincareShop.Models.Interfaces;

namespace KoreanSkincareShop.Models.Services.Listeners;

public class PaymentInventoryUpdateService : IDisposable
{
    private readonly ILogger<PaymentInventoryUpdateService> _logger;
    private readonly IPaymentService _paymentService;
    private bool _disposed;

    public PaymentInventoryUpdateService(
        ILogger<PaymentInventoryUpdateService> logger,
        IPaymentService paymentService)
    {
        _logger = logger;
        _paymentService = paymentService;

        _paymentService.PaymentCompleted += OnPaymentCompleted;
        _paymentService.PaymentRefunded += OnPaymentRefunded;
    }

    private void OnPaymentCompleted(object? sender, PaymentCompletedEventArgs e)
    {
        _logger.LogInformation("[INVENTORY] Updating inventory for Order #{OrderId}", e.Payment.OrderId);
        UpdateInventory(e.Payment.OrderId, isRefund: false);
    }

    private void OnPaymentRefunded(object? sender, PaymentRefundedEventArgs e)
    {
        _logger.LogInformation("[INVENTORY] Restoring inventory for refunded Order #{OrderId}", e.Payment.OrderId);
        UpdateInventory(e.Payment.OrderId, isRefund: true);
    }

    private void UpdateInventory(int orderId, bool isRefund)
    {
        // Demo: log instead of updating real inventory
        var action = isRefund ? "restored to" : "deducted from";
        _logger.LogInformation("Inventory {Action} stock for Order #{OrderId}", action, orderId);
    }

    public void Dispose()
    {
        if (_disposed) return;

        _paymentService.PaymentCompleted -= OnPaymentCompleted;
        _paymentService.PaymentRefunded -= OnPaymentRefunded;

        _disposed = true;
    }
}
