using ksoproject.Models;

public interface ICartService
{
    Task AddToCartAsync(int productId, int quantity);
    Task RemoveFromCartAsync(int productId);
    Task UpdateQuantityAsync(int productId, int quantity);
    Task<List<CartItem>> GetCartItemsAsync();
    Task<int> GetCartItemCountAsync();
    Task<decimal> GetCartTotalAsync();
    Task ClearCartAsync();
}
