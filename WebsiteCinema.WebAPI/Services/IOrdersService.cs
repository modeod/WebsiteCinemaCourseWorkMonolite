using WebsiteCinema.WebAPI.Models;

namespace WebsiteCinema.WebAPI.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string UserId, string UserEmailAddress);

        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}
