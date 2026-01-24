using KoreanSkincareShop.Models;

namespace KoreanSkincareShop.Events;

public class PaymentEventArgs : EventArgs
{
    public Payment Payment { get; }
    public DateTime Timestamp { get; }

    public PaymentEventArgs(Payment payment)
    {
        Payment = payment;
        Timestamp = DateTime.Now;
    }
}

public class PaymentInitiatedEventArgs : PaymentEventArgs
{
    public PaymentInitiatedEventArgs(Payment payment) : base(payment)
    {
    }
}

public class PaymentProcessingEventArgs : PaymentEventArgs
{
    public PaymentProcessingEventArgs(Payment payment) : base(payment)
    {
    }
}

public class PaymentCompletedEventArgs : PaymentEventArgs
{
    public string TransactionId { get; }

    public PaymentCompletedEventArgs(Payment payment, string transactionId) : base(payment)
    {
        TransactionId = transactionId;
    }
}

public class PaymentFailedEventArgs : PaymentEventArgs
{
    public string FailureReason { get; }

    public PaymentFailedEventArgs(Payment payment, string failureReason) : base(payment)
    {
        FailureReason = failureReason;
    }
}

public class PaymentRefundedEventArgs : PaymentEventArgs
{
    public decimal RefundAmount { get; }
    public string RefundReason { get; }

    public PaymentRefundedEventArgs(Payment payment, decimal refundAmount, string refundReason) : base(payment)
    {
        RefundAmount = refundAmount;
        RefundReason = refundReason;
    }
}
