using KoreanSkincareShop.Events;
using KoreanSkincareShop.Models.Interfaces;

namespace KoreanSkincareShop.Models.Services.Listeners;

public class PaymentEmailNotificationService : IDisposable
{
    private readonly ILogger<PaymentEmailNotificationService> _logger;
    private readonly IPaymentService _paymentService;
    private bool _disposed;

    public PaymentEmailNotificationService(
        ILogger<PaymentEmailNotificationService> logger,
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
        _logger.LogInformation("[EMAIL] Payment initiated for Order #{OrderId}", e.Payment.OrderId);

        SendEmail(
            to: e.Payment.Order?.Email ?? "customer@example.com",
            subject: "Payment Initiated",
            body: $"Your payment of {e.Payment.Amount:C} has been initiated. Order #{e.Payment.OrderId}"
        );
    }

    private void OnPaymentCompleted(object? sender, PaymentCompletedEventArgs e)
    {
        _logger.LogInformation(
            "[EMAIL] Payment completed for Order #{OrderId}. Transaction: {TransactionId}",
            e.Payment.OrderId,
            e.TransactionId);

        SendEmail(
            to: e.Payment.Order?.Email ?? "customer@example.com",
            subject: "Payment Successful!",
            body: $"""
                Thank you for your payment!

                Order Number: #{e.Payment.OrderId}
                Amount: {e.Payment.Amount:C}
                Transaction ID: {e.TransactionId}

                Your order is being processed.
                """
        );
    }

    private void OnPaymentFailed(object? sender, PaymentFailedEventArgs e)
    {
        _logger.LogWarning(
            "[EMAIL] Payment failed for Order #{OrderId}. Reason: {FailureReason}",
            e.Payment.OrderId,
            e.FailureReason);

        SendEmail(
            to: e.Payment.Order?.Email ?? "customer@example.com",
            subject: "Payment Failed",
            body: $"""
                Unfortunately, your payment could not be processed.

                Order Number: #{e.Payment.OrderId}
                Amount: {e.Payment.Amount:C}
                Reason: {e.FailureReason}

                Please try again or contact support.
                """
        );
    }

    private void OnPaymentRefunded(object? sender, PaymentRefundedEventArgs e)
    {
        _logger.LogInformation(
            "[EMAIL] Payment refunded for Order #{OrderId}. Amount: {RefundAmount:C}",
            e.Payment.OrderId,
            e.RefundAmount);

        SendEmail(
            to: e.Payment.Order?.Email ?? "customer@example.com",
            subject: "Refund Processed",
            body: $"""
                Your refund has been processed.

                Order Number: #{e.Payment.OrderId}
                Refund Amount: {e.RefundAmount:C}
                Reason: {e.RefundReason}

                Please allow 5-10 business days for the refund to appear in your account.
                """
        );
    }

    private void SendEmail(string to, string subject, string body)
    {
        // Demo: log instead of sending real email
        _logger.LogInformation("Email sent to {To}: {Subject}", to, subject);
        _logger.LogDebug("Email body: {Body}", body);
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
