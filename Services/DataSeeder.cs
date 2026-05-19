using Microsoft.EntityFrameworkCore;

namespace ksoproject.Services;

public class DataSeeder
{
    private readonly AppDbContext _context;
    private readonly Random _random = new();

    public DataSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedDataAsync()
    {
        // Check if products exist
        if (!await _context.Products.AnyAsync())
        {
            await SeedProductsAsync();
        }

        // Check if advertisements exist
        if (!await _context.Ads.AnyAsync())
        {
            await SeedAdvertisementsAsync();
        }
    }

    private async Task SeedProductsAsync()
    {
        List<Product> products = [
            new() {
                Name = "Кофта stone island",
                Description = "Отличное предложение",
                ImagePath = "/img/1.jpg",
                Price = 25000
            },
            new() {
                Name = "Футболка supreme",
                Description = "Низкая цена",
                ImagePath = "/img/2.jpg",
                Price = 9999
            },
            new() {
                Name = "Кепка Balenciaga",
                Description = "Хороший выбор на лето",
                ImagePath = "/img/3.jpg",
                Price = 25000
            },
            new() {
                Name = "Носки Gucci",
                Description = "Отлично сидят",
                ImagePath = "/img/4.jpg",
                Price = 7999
            },
            new() {
                Name = "Браслет lacoste",
                Description = "Для деловых встреч",
                ImagePath = "/img/5.jpg",
                Price = 6700
            },
            new() {
                Name = "Штаны C.P. Company",
                Description = "Идеальный вариант",
                ImagePath = "/img/6.jpg",
                Price = 13999
            },
            new() {
                Name = "Шорты Nike",
                Description = "Отдых на море будет лучшим в них",
                ImagePath = "/img/7.jpg",
                Price = 2999
            },
            new() {
                Name = "Браслет Van Cleef",
                Description = "Для тех, кого ценят",
                ImagePath = "/img/8.jpg",
                Price = 650000
            }
        ];
        await _context.Products.AddRangeAsync(products);
        await _context.SaveChangesAsync();
    }

    private async Task SeedAdvertisementsAsync()
    {
        var products = await _context.Products.ToListAsync();
        if (!products.Any()) return;

        var advertisements = new List<Advertisement>();

        // Create 3-5 advertisements
        int adCount = _random.Next(3, 6);
        var usedProducts = new HashSet<int>();

        for (int i = 0; i < adCount && i < products.Count; i++)
        {
            // Select random product that hasn't been used
            Product selectedProduct;
            do
            {
                selectedProduct = products[_random.Next(products.Count)];
            } while (usedProducts.Contains(selectedProduct.Id) && usedProducts.Count < products.Count);

            usedProducts.Add(selectedProduct.Id);

            var advertisement = new Advertisement
            {
                ProductId = selectedProduct.Id,
                Product = selectedProduct
            };
            advertisements.Add(advertisement);
        }

        await _context.Ads.AddRangeAsync(advertisements);
        await _context.SaveChangesAsync();
    }
}
