using KoreanSkincareShop.Data;
using KoreanSkincareShop.Models.Interfaces;

namespace KoreanSkincareShop.Models.Services;

public class ProductRepository : IProductRepository
{
    private SkincareShopDbContext dbContext;

    public ProductRepository(SkincareShopDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public IEnumerable<Product> GetAllProducts()
    {
        return dbContext.Products;
    }

    public IEnumerable<Product> GetTrendingProducts()
    {
        return dbContext.Products.Where(p => p.IsTrendingProduct);
    }

    public Product? GetProductDetail(int id)
    {
        return dbContext.Products.FirstOrDefault(p => p.Id == id);
    }
}