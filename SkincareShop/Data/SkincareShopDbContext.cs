namespace KoreanSkincareShop.Data;

using KoreanSkincareShop.Models;
using Microsoft.EntityFrameworkCore;

public class SkincareShopDbContext : DbContext
{
    public SkincareShopDbContext(DbContextOptions<SkincareShopDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>().HasData(

            new Product
            {
                Id = 1,
                Name = "Mediheal Cica AC Calming Sleeping Mask",
                Details = "Overnight calming mask with cica and niacinamide that helps reduce irritation and restore skin balance.",
                Price = 12.99m,
                IsTrendingProduct = true,
                ImageUrl = "/images/products/mediheal-cica-ac-calming-sleeping-mask.webp"
            },

            new Product
            {
                Id = 2,
                Name = "Mediheal Collagen Ampoule Pad",
                Details = "Firming toner pads infused with collagen to improve skin elasticity and hydration.",
                Price = 119.99m,
                IsTrendingProduct = true,
                ImageUrl = "/images/products/mediheal-collagen-ampoule-pad.jpg"
            },

            new Product
            {
                Id = 3,
                Name = "Mediheal Derma Clear Madecassoside Patch",
                Details = "Targeted blemish patches with madecassoside to calm and protect irritated skin.",
                Price = 25.99m,
                IsTrendingProduct = false,
                ImageUrl = "/images/products/mediheal-derma-clear-madecassoside-patch.webp"
            },

            new Product
            {
                Id = 4,
                Name = "Mediheal Derma Modeling Madecassoside Mask",
                Details = "Alginate modeling mask with madecassoside for intensive soothing and regeneration.",
                Price = 31.99m,
                IsTrendingProduct = false,
                ImageUrl = "/images/products/mediheal-derma-modeling-madecassoside-mask.webp"
            },

            new Product
            {
                Id = 5,
                Name = "Mediheal Derma Modeling Teatree Mask",
                Details = "Purifying alginate mask with tea tree extract for oily and blemish-prone skin.",
                Price = 31.99m,
                IsTrendingProduct = false,
                ImageUrl = "/images/products/mediheal-derma-modeling-teatree-mask.webp"
            },

            new Product
            {
                Id = 6,
                Name = "Mediheal Essential Mask Set",
                Details = "Set of five essential Mediheal sheet masks for different skin needs.",
                Price = 53.99m,
                IsTrendingProduct = true,
                ImageUrl = "/images/products/mediheal-essential-mask-set.webp"
            },

            new Product
            {
                Id = 7,
                Name = "Mediheal Madecassoside Blemish Pad",
                Details = "Soothing toner pads with madecassoside to reduce redness and improve skin texture.",
                Price = 119.99m,
                IsTrendingProduct = true,
                ImageUrl = "/images/products/mediheal-madecassoside-blemish-pad.jpg"
            },

            new Product
            {
                Id = 8,
                Name = "Mediheal Madecassoside Essential Mask",
                Details = "Regenerating sheet mask with madecassoside for sensitive and irritated skin.",
                Price = 7.99m,
                IsTrendingProduct = false,
                ImageUrl = "/images/products/mediheal-madecassoside-essential-mask.webp"
            },

            new Product
            {
                Id = 9,
                Name = "Mediheal N.M.F Panthenol Mask",
                Details = "Deeply moisturizing sheet mask with panthenol to strengthen the skin barrier.",
                Price = 9.99m,
                IsTrendingProduct = false,
                ImageUrl = "/images/products/mediheal-nmf-panthenol-mask.webp"
            },

            new Product
            {
                Id = 10,
                Name = "Mediheal Pure Calming Mask",
                Details = "Daily calming sheet mask designed to soothe sensitive and stressed skin.",
                Price = 5.99m,
                IsTrendingProduct = true,
                ImageUrl = "/images/products/mediheal-pure-calming-mask.jpg"
            },

            new Product
            {
                Id = 11,
                Name = "Mediheal Teatree Essential Mask",
                Details = "Bamboo sheet mask with tea tree extract to calm and purify the skin.",
                Price = 5.95m,
                IsTrendingProduct = true,
                ImageUrl = "/images/products/mediheal-teatree-essential-mask.webp"
            },

            new Product
            {
                Id = 12,
                Name = "Mediheal Teatree Nude Gel Mask",
                Details = "Hydrogel calming mask with tea tree for instant soothing effect.",
                Price = 10.99m,
                IsTrendingProduct = false,
                ImageUrl = "/images/products/mediheal-teatree-nude-gel-mask.jpg"
            },

            new Product
            {
                Id = 13,
                Name = "Mediheal Teatree Trouble Pad",
                Details = "Exfoliating and calming pads with tea tree extract for troubled skin.",
                Price = 119.99m,
                IsTrendingProduct = true,
                ImageUrl = "/images/products/mediheal-teatree-trouble-pad.jpg"
            },

            new Product
            {
                Id = 14,
                Name = "Mediheal Teatree X Cica Mask",
                Details = "Soothing sheet mask combining tea tree and cica for irritated skin.",
                Price = 9.99m,
                IsTrendingProduct = false,
                ImageUrl = "/images/products/mediheal-teatree-x-cica-mask.webp"
            },
            new Product
            {
                Id = 15,
                Name = "Mediheal Teatree Essential Bamboo Mask",
                Details = "Calming bamboo sheet mask with tea tree extract, ideal for sensitive and irritated skin.",
                Price = 7.99m,
                IsTrendingProduct = true,
                ImageUrl = "/images/products/mediheal-teatree-essential-mask.jpg"
            }
        );
    }
}