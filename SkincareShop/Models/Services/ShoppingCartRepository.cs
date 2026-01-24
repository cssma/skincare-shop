using KoreanSkincareShop.Data;
using KoreanSkincareShop.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KoreanSkincareShop.Models.Services;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly SkincareShopDbContext _dbContext;
    private readonly string _shoppingCartId;

    public ShoppingCartRepository(SkincareShopDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;

        var session = httpContextAccessor.HttpContext?.Session;
        if (session == null)
        {
            throw new InvalidOperationException("No HTTP session available");
        }

        var cartId = session.GetString("CartId");
        if (string.IsNullOrEmpty(cartId))
        {
            cartId = Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
        }

        _shoppingCartId = cartId;
    }

    public string ShoppingCartId => _shoppingCartId;

    public void AddToCart(Product product)
    {
        var shoppingCartItem = _dbContext.ShoppingCartItems
            .Include(s => s.Product)
            .FirstOrDefault(s => s.Product != null
                && s.Product.Id == product.Id
                && s.ShoppingCartId == _shoppingCartId);

        if (shoppingCartItem == null)
        {
            shoppingCartItem = new ShoppingCartItem
            {
                ShoppingCartId = _shoppingCartId,
                Product = product,
                Qty = 1
            };
            _dbContext.ShoppingCartItems.Add(shoppingCartItem);
        }
        else
        {
            shoppingCartItem.Qty++;
        }

        _dbContext.SaveChanges();
    }

    public int RemoveFromCart(Product product)
    {
        var shoppingCartItem = _dbContext.ShoppingCartItems
            .Include(s => s.Product)
            .FirstOrDefault(s => s.Product != null
                && s.Product.Id == product.Id
                && s.ShoppingCartId == _shoppingCartId);

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
                _dbContext.ShoppingCartItems.Remove(shoppingCartItem);
            }
        }

        _dbContext.SaveChanges();
        return quantity;
    }

    public List<ShoppingCartItem> GetShoppingCartItems()
    {
        return _dbContext.ShoppingCartItems
            .Where(s => s.ShoppingCartId == _shoppingCartId)
            .Include(s => s.Product)
            .ToList();
    }

    public void ClearCart()
    {
        var cartItems = _dbContext.ShoppingCartItems
            .Where(s => s.ShoppingCartId == _shoppingCartId);

        _dbContext.ShoppingCartItems.RemoveRange(cartItems);
        _dbContext.SaveChanges();
    }

    public decimal GetShoppingCartTotal()
    {
        return _dbContext.ShoppingCartItems
            .Where(s => s.ShoppingCartId == _shoppingCartId)
            .Include(s => s.Product)
            .Where(s => s.Product != null)
            .Sum(s => s.Product!.Price * s.Qty);
    }
}
