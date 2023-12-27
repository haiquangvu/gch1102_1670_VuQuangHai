using BookEshop.Data;
using BookEshop.Infrastructure;
using BookEshop.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace BookEshop.Controllers
{
    public class CartController : Controller
    {
        public Cart? Cart { get; set; }
        private readonly ApplicationDbContext _context;
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View("Cart", HttpContext.Session.GetJson<Cart>("cart"));
        }
        public IActionResult AddToCart( int productId)
        {
            Product? product = _context.Products
            .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View("Cart", Cart);
        }
        public IActionResult UpdateCart(int productId)
        {
            Product? product = _context.Products
            .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, -1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View("Cart", Cart);
        }
        public IActionResult RemoveFromCart(int productId)
        {
            Product? product = _context.Products
            .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ;
                Cart.RemoveLine(product);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View("Cart", Cart);
        }
        public IActionResult OrderConfirmation()
        {
            // The ViewBag.OrderId has been set in the Checkout action
            return View();
        }

        public IActionResult Checkout()
        {
            Cart = HttpContext.Session.GetJson<Cart>("cart");
            if (Cart == null || Cart.Lines.Count == 0)
            {
                // Handle empty cart scenario
                return RedirectToAction("Index");
            }

            // Create a new order
            Order order = new Order();

            // Populate order lines from the cart and compute total
            foreach (var line in Cart.Lines)
            {
                order.OrderLines.Add(new OrderLine
                {
                    ProductName = line.Product.ProductName,
                    Quantity = line.Quantity,
                    Total = (decimal)line.Product.ProductPrice * line.Quantity
                });

                order.Total += (decimal)line.Product.ProductPrice * line.Quantity;
            }

            // Save the order to the database
            _context.Orders.Add(order);
            _context.SaveChanges();

            // Clear the cart
            Cart.Clear();
            HttpContext.Session.SetJson("cart", Cart);

            // You can redirect to an order confirmation page or any other relevant page
            return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
        }

    }
}
