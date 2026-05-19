using ksoproject.Models;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(string userId, List<CartItem> cartItems, string shippingAddress, string paymentMethod);
    Task<List<Order>> GetUserOrdersAsync(string userId);
    Task<Order?> GetOrderByIdAsync(int orderId, string userId);
    Task<bool> CancelOrderAsync(int orderId, string userId);
    Task UpdateOrderStatusAsync(int orderId, string status);
}

public class CartItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
