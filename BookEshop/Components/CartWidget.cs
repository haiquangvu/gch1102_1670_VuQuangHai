using BookEshop.Data;
using BookEshop.Infrastructure;
using BookEshop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookEshop.Components
{
    public class CartWidget:ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View(HttpContext.Session.GetJson<Cart>("cart"));
        }
    }
}
