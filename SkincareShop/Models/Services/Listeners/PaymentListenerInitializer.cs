namespace KoreanSkincareShop.Models.Services.Listeners;

public class PaymentListenerInitializer
{
    public PaymentListenerInitializer(
        PaymentEmailNotificationService emailService,
        PaymentAnalyticsService analyticsService,
        PaymentInventoryUpdateService inventoryService)
    {
        // Dependencies are injected to ensure they're instantiated and subscribed
        // The listeners subscribe to IPaymentService events in their constructors
    }
}
