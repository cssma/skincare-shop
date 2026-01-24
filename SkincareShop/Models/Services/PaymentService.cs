using KoreanSkincareShop.Data;
using KoreanSkincareShop.Events;
using KoreanSkincareShop.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KoreanSkincareShop.Models.Services;

public class PaymentService : IPaymentService
{
    private readonly SkincareShopDbContext _context;
    private readonly TimeProvider _timeProvider;
    private readonly ILogger<PaymentService> _logger;

    public event EventHandler<PaymentInitiatedEventArgs>? PaymentInitiated;
    public event EventHandler<PaymentProcessingEventArgs>? PaymentProcessing;
    public event EventHandler<PaymentCompletedEventArgs>? PaymentCompleted;
    public event EventHandler<PaymentFailedEventArgs>? PaymentFailed;
    public event EventHandler<PaymentRefundedEventArgs>? PaymentRefunded;

    public PaymentService(
        SkincareShopDbContext context,
        TimeProvider timeProvider,
        ILogger<PaymentService> logger)
    {
        _context = context;
        _timeProvider = timeProvider;
        _logger = logger;
    }

    public async Task<Payment> InitiatePaymentAsync(Order order, PaymentMethod paymentMethod)
    {
        var payment = new Payment
        {
            OrderId = order.Id,
            Order = order,
            Amount = order.OrderTotal,
            Status = PaymentStatus.Pending,
            PaymentMethod = paymentMethod,
            CreatedAt = _timeProvider.GetLocalNow().DateTime
        };

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        RaiseEventSafely(() => OnPaymentInitiated(new PaymentInitiatedEventArgs(payment)));

        return payment;
    }

    public async Task<Payment> ProcessPaymentAsync(int paymentId)
    {
        var payment = await _context.Payments
            .Include(p => p.Order)
            .FirstOrDefaultAsync(p => p.Id == paymentId);

        if (payment == null)
        {
            throw new InvalidOperationException("Payment does not exist.");
        }

        if (payment.Status != PaymentStatus.Pending)
        {
            return payment;
        }

        payment.Status = PaymentStatus.Processing;
        await _context.SaveChangesAsync();

        RaiseEventSafely(() => OnPaymentProcessing(new PaymentProcessingEventArgs(payment)));

        // Demo: simulate gateway delay
        await Task.Delay(500);

        // Demo: always succeed
        payment.Status = PaymentStatus.Completed;
        payment.ProcessedAt = _timeProvider.GetLocalNow().DateTime;
        payment.TransactionId = GenerateTransactionId();

        await _context.SaveChangesAsync();

        RaiseEventSafely(() => OnPaymentCompleted(
            new PaymentCompletedEventArgs(payment, payment.TransactionId)));

        return payment;
    }

    public async Task<Payment> RefundPaymentAsync(int paymentId, string reason)
    {
        var payment = await _context.Payments
            .Include(p => p.Order)
            .FirstOrDefaultAsync(p => p.Id == paymentId);

        if (payment == null)
        {
            throw new ArgumentException("Payment not found", nameof(paymentId));
        }

        if (payment.Status != PaymentStatus.Completed)
        {
            throw new InvalidOperationException("Only completed payments can be refunded");
        }

        payment.Status = PaymentStatus.Refunded;
        await _context.SaveChangesAsync();

        RaiseEventSafely(() => OnPaymentRefunded(
            new PaymentRefundedEventArgs(payment, payment.Amount, reason)));

        return payment;
    }

    public async Task<Payment?> GetPaymentByIdAsync(int paymentId)
    {
        return await _context.Payments
            .Include(p => p.Order)
            .FirstOrDefaultAsync(p => p.Id == paymentId);
    }

    public async Task<Payment?> GetPaymentByOrderIdAsync(int orderId)
    {
        return await _context.Payments
            .Include(p => p.Order)
            .FirstOrDefaultAsync(p => p.OrderId == orderId);
    }

    private void RaiseEventSafely(Action raiseEvent)
    {
        try
        {
            raiseEvent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Payment event listener threw an exception");
        }
    }

    protected virtual void OnPaymentInitiated(PaymentInitiatedEventArgs e)
    {
        PaymentInitiated?.Invoke(this, e);
    }

    protected virtual void OnPaymentProcessing(PaymentProcessingEventArgs e)
    {
        PaymentProcessing?.Invoke(this, e);
    }

    protected virtual void OnPaymentCompleted(PaymentCompletedEventArgs e)
    {
        PaymentCompleted?.Invoke(this, e);
    }

    protected virtual void OnPaymentFailed(PaymentFailedEventArgs e)
    {
        PaymentFailed?.Invoke(this, e);
    }

    protected virtual void OnPaymentRefunded(PaymentRefundedEventArgs e)
    {
        PaymentRefunded?.Invoke(this, e);
    }

    private string GenerateTransactionId()
    {
        var timestamp = _timeProvider.GetLocalNow().ToString("yyyyMMddHHmmss");
        var guid = Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
        return $"TXN-{timestamp}-{guid}";
    }
}
