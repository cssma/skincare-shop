using KoreanSkincareShop.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KoreanSkincareShop.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository productRepository;
        public HomeController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public ActionResult Index()
        {
            return View(productRepository.GetTrendingProducts());
        }

    }
}
