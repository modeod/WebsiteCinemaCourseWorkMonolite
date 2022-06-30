using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebsiteCinema.Data.Cart;
using WebsiteCinema.Data.Services;
using WebsiteCinema.Data.Static;
using WebsiteCinema.Services;
using WebsiteCinema.ViewModels;

namespace WebsiteCinema.Controllers
{
    [Route("Orders")]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;

        public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _moviesService = moviesService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        [Route("/Orders")]
        [Route("OrdersList")]
        public async Task<IActionResult> OrdersList()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);

            return View(orders);
        }

        [Route("/Cart")]
        public IActionResult Cart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var responce = new VMShoppingCart()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View("Cart",responce);
        }

        [Route("AddItemToShoppingCart")]
        public async Task<RedirectToActionResult> AddItemToShoppingCart([FromQuery] int id)
        {
            var movie = await _moviesService.GetByIdAsync(id);
            if(movie != null)
                _shoppingCart.AddItemToCart(movie);
             
            return RedirectToAction(nameof(Cart));
        }

        [Route("RemoveItemFromShoppingCart")]
        public async Task<RedirectToActionResult> RemoveItemFromShoppingCart([FromQuery] int id)
        {
            var movie = await _moviesService.GetByIdAsync(id);
            if (movie != null)
                _shoppingCart.RemoveItemFromCart(movie);

            return RedirectToAction(nameof(Cart));
        }

        [Route("CompleteOrder")]
        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }

    }
}
