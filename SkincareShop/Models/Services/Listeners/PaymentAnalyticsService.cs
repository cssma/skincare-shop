using System.Text.Json;
using KoreanSkincareShop.Events;
using KoreanSkincareShop.Models.Interfaces;

namespace KoreanSkincareShop.Models.Services.Listeners;

public class PaymentAnalyticsService : IDisposable
{
    private readonly ILogger<PaymentAnalyticsService> _logger;
    private readonly IPaymentService _paymentService;
    private readonly object _lock = new();
    private bool _disposed;

    private int _totalPaymentsInitiated;
    private int _totalPaymentsCompleted;
    private int _totalPaymentsFailed;
    private decimal _totalRevenue;

    public PaymentAnalyticsService(
        ILogger<PaymentAnalyticsService> logger,
        IPaymentService paymentService)
    {
        _logger = logger;
        _paymentService = paymentService;

        _paymentService.PaymentInitiated += OnPaymentInitiated;
        _paymentService.PaymentCompleted += OnPaymentCompleted;
        _paymentService.PaymentFailed += OnPaymentFailed;
        _paymentService.PaymentRefunded += OnPaymentRefunded;
    }

    private void OnPaymentInitiated(object? sender, PaymentInitiatedEventArgs e)
    {
        int total;
        lock (_lock)
        {
            _totalPaymentsInitiated++;
            total = _totalPaymentsInitiated;
        }

        _logger.LogInformation("[ANALYTICS] Payment initiated. Total initiated: {Total}", total);

        TrackEvent("payment_initiated", new
        {
            order_id = e.Payment.OrderId,
            amount = e.Payment.Amount,
            payment_method = e.Payment.PaymentMethod.ToString()
        });
    }

    private void OnPaymentCompleted(object? sender, PaymentCompletedEventArgs e)
    {
        double successRate;
        decimal revenue;

        lock (_lock)
        {
            _totalPaymentsCompleted++;
            _totalRevenue += e.Payment.Amount;
            successRate = GetSuccessRateUnsafe();
            revenue = _totalRevenue;
        }

        _logger.LogInformation(
            "[ANALYTICS] Payment completed. Success rate: {SuccessRate:F2}%. Total revenue: {Revenue:C}",
            successRate,
            revenue);

        TrackEvent("payment_completed", new
        {
            order_id = e.Payment.OrderId,
            amount = e.Payment.Amount,
            transaction_id = e.TransactionId,
            payment_method = e.Payment.PaymentMethod.ToString()
        });
    }

    private void OnPaymentFailed(object? sender, PaymentFailedEventArgs e)
    {
        double failureRate;

        lock (_lock)
        {
            _totalPaymentsFailed++;
            failureRate = GetFailureRateUnsafe();
        }

        _logger.LogWarning("[ANALYTICS] Payment failed. Failure rate: {FailureRate:F2}%", failureRate);

        TrackEvent("payment_failed", new
        {
            order_id = e.Payment.OrderId,
            amount = e.Payment.Amount,
            failure_reason = e.FailureReason,
            payment_method = e.Payment.PaymentMethod.ToString()
        });
    }

    private void OnPaymentRefunded(object? sender, PaymentRefundedEventArgs e)
    {
        decimal revenue;

        lock (_lock)
        {
            _totalRevenue -= e.RefundAmount;
            revenue = _totalRevenue;
        }

        _logger.LogInformation("[ANALYTICS] Payment refunded. Adjusted revenue: {Revenue:C}", revenue);

        TrackEvent("payment_refunded", new
        {
            order_id = e.Payment.OrderId,
            refund_amount = e.RefundAmount,
            refund_reason = e.RefundReason
        });
    }

    // Must be called while holding _lock
    private double GetSuccessRateUnsafe()
    {
        int totalProcessed = _totalPaymentsCompleted + _totalPaymentsFailed;
        return totalProcessed > 0 ? _totalPaymentsCompleted * 100.0 / totalProcessed : 0;
    }

    // Must be called while holding _lock
    private double GetFailureRateUnsafe()
    {
        int totalProcessed = _totalPaymentsCompleted + _totalPaymentsFailed;
        return totalProcessed > 0 ? _totalPaymentsFailed * 100.0 / totalProcessed : 0;
    }

    private void TrackEvent(string eventName, object properties)
    {
        // Demo: log instead of sending to real analytics
        _logger.LogDebug(
            "Analytics Event: {EventName}, Properties: {Properties}",
            eventName,
            JsonSerializer.Serialize(properties));
    }

    public int GetTotalPaymentsInitiated()
    {
        lock (_lock) return _totalPaymentsInitiated;
    }

    public int GetTotalPaymentsCompleted()
    {
        lock (_lock) return _totalPaymentsCompleted;
    }

    public int GetTotalPaymentsFailed()
    {
        lock (_lock) return _totalPaymentsFailed;
    }

    public decimal GetTotalRevenue()
    {
        lock (_lock) return _totalRevenue;
    }

    public void Dispose()
    {
        if (_disposed) return;

        _paymentService.PaymentInitiated -= OnPaymentInitiated;
        _paymentService.PaymentCompleted -= OnPaymentCompleted;
        _paymentService.PaymentFailed -= OnPaymentFailed;
        _paymentService.PaymentRefunded -= OnPaymentRefunded;

        _disposed = true;
    }
}
