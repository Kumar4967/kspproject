using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Text.Json;
using ksoproject.Models;

public class CartService : ICartService
{
    private readonly LocalStorageService _localStorage;
    private readonly AppDbContext _context;
    private readonly ILogger<CartService> _logger;
    private const string CartKey = "shopping_cart";

    public CartService(LocalStorageService localStorage, AppDbContext context, ILogger<CartService> logger)
    {
        _localStorage = localStorage;
        _context = context;
        _logger = logger;
    }

    public async Task AddToCartAsync(int productId, int quantity)
    {
        try
        {
            var cart = await GetCartFromStorageAsync();
            var existingItem = cart.FirstOrDefault(x => x.ProductId == productId);

            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = quantity
                });
            }

            await SaveCartToStorageAsync(cart);
            _logger.LogInformation("Added product {ProductId} to cart", productId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding product {ProductId} to cart", productId);
            throw;
        }
    }

    public async Task<List<CartItem>> GetCartItemsAsync()
    {
        return await GetCartFromStorageAsync();
    }

    public async Task<int> GetCartItemCountAsync()
    {
        var cart = await GetCartFromStorageAsync();
        return cart.Sum(x => x.Quantity);
    }

    public async Task<decimal> GetCartTotalAsync()
    {
        var cart = await GetCartFromStorageAsync();
        return cart.Sum(x => x.Total);
    }

    public async Task ClearCartAsync()
    {
        await SaveCartToStorageAsync(new List<CartItem>());
    }

    public async Task RemoveFromCartAsync(int productId)
    {
        var cart = await GetCartFromStorageAsync();
        var item = cart.FirstOrDefault(x => x.ProductId == productId);
        if (item != null)
        {
            cart.Remove(item);
            await SaveCartToStorageAsync(cart);
        }
    }

    public async Task UpdateQuantityAsync(int productId, int quantity)
    {
        var cart = await GetCartFromStorageAsync();
        var item = cart.FirstOrDefault(x => x.ProductId == productId);
        if (item != null)
        {
            if (quantity <= 0)
            {
                cart.Remove(item);
            }
            else
            {
                item.Quantity = quantity;
            }
            await SaveCartToStorageAsync(cart);
        }
    }

    private async Task<List<CartItem>> GetCartFromStorageAsync()
    {
        try
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>(CartKey);
            return cart ?? new List<CartItem>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading cart from storage");
            return new List<CartItem>();
        }
    }

    private async Task SaveCartToStorageAsync(List<CartItem> cart)
    {
        try
        {
            await _localStorage.SetItemAsync(CartKey, cart);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving cart to storage");
            throw;
        }
    }
}
