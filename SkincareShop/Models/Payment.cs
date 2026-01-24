namespace KoreanSkincareShop.Models;

public class Payment
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? TransactionId { get; set; }
    public string? FailureReason { get; set; }
}

public enum PaymentStatus
{
    Pending,
    Processing,
    Completed,
    Failed,
    Refunded
}

public enum PaymentMethod
{
    CreditCard,
    DebitCard,
    PayPal,
    BankTransfer,
    Blik
}