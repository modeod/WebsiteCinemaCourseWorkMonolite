using Microsoft.AspNetCore.Mvc;
using WebsiteCinema.Data.Cart;

namespace WebsiteCinema.Data.ViewComponents
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View(0);
            }

            var items = _shoppingCart.GetShoppingCartItems();
            return View(items.Select(x => x.Amount).Sum(x => x));
        }
    }
}
