namespace KoreanSkincareShop.Models.Interfaces;

using KoreanSkincareShop.Events;

public interface IPaymentService
{
    // Events - kluczowa część używająca delegatów!
    event EventHandler<PaymentInitiatedEventArgs>? PaymentInitiated;
    event EventHandler<PaymentProcessingEventArgs>? PaymentProcessing;
    event EventHandler<PaymentCompletedEventArgs>? PaymentCompleted;
    event EventHandler<PaymentFailedEventArgs>? PaymentFailed;
    event EventHandler<PaymentRefundedEventArgs>? PaymentRefunded;

    // Metody
    Task<Payment> InitiatePaymentAsync(Order order, PaymentMethod paymentMethod);
    Task<Payment> ProcessPaymentAsync(int paymentId);
    Task<Payment> RefundPaymentAsync(int paymentId, string reason);
    Task<Payment?> GetPaymentByIdAsync(int paymentId);
    Task<Payment?> GetPaymentByOrderIdAsync(int orderId);
}