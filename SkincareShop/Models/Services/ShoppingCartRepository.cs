using KoreanSkincareShop.Data;
using KoreanSkincareShop.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KoreanSkincareShop.Models.Services;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private SkincareShopDbContext dbContext;
    public ShoppingCartRepository(SkincareShopDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public List<ShoppingCartItem>? ShoppingCartItems { get; set; }
    
    public string? ShoppingCartId { get; set; }

    public static ShoppingCartRepository GetCart(IServiceProvider services)
    {
        ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
        SkincareShopDbContext context = services.GetRequiredService<SkincareShopDbContext>() ?? throw new Exception("Error initializing skincareshopdbcontext");
        string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
        session?.SetString("CartId", cartId);
        return new ShoppingCartRepository(context) { ShoppingCartId = cartId };
    }

    public void AddToCart(Product product)
    {
        var shoppingCartItem = dbContext.ShoppingCartItems.FirstOrDefault(s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);
        if (shoppingCartItem == null)
        {
            shoppingCartItem = new ShoppingCartItem()
            {
                ShoppingCartId = ShoppingCartId,
                Product = product,
                Qty = 1
            };
            dbContext.ShoppingCartItems.Add(shoppingCartItem);
        }
        else
        {
            shoppingCartItem.Qty++;
        }
        dbContext.SaveChanges();
    }


    public int RemoveFromCart(Product product)
    {
        var shoppingCartItem = dbContext.ShoppingCartItems.FirstOrDefault(s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);
        var quantity = 0;
        if (shoppingCartItem != null)
        {
            if (shoppingCartItem.Qty > 1)
            {
                shoppingCartItem.Qty--;
                quantity = shoppingCartItem.Qty;
            }
            else
            {
                dbContext.ShoppingCartItems.Remove(shoppingCartItem);
            }
        }
        dbContext.SaveChanges();
        return quantity;
    }

    public List<ShoppingCartItem> GetShoppingCartItems()
    {
        return ShoppingCartItems ??= dbContext.ShoppingCartItems.Where(s => s.ShoppingCartId == ShoppingCartId)
            .Include(p => p.Product).ToList();
    }

    public void ClearCart()
    {
        var cartItems = dbContext.ShoppingCartItems.Where(s => s.ShoppingCartId == ShoppingCartId);
        dbContext.ShoppingCartItems.RemoveRange(cartItems);
        dbContext.SaveChanges();
    }

    public decimal GetShoppingCartTotal()
    {
        var totalCost = dbContext.ShoppingCartItems.Where(s => s.ShoppingCartId == ShoppingCartId)
            .Select(s => s.Product.Price * s.Qty).Sum();
        return totalCost;
    }

}