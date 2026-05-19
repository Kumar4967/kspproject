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
        var products = new List<Product>();
        var productNames = new[]
        {
            "Смартфон Galaxy S23", "Ноутбук MacBook Pro", "Наушники Sony WH-1000XM5",
            "Клавиатура Mechanical", "Мышь Logitech MX Master", "Монитор Dell UltraSharp",
            "Внешний SSD 1TB", "Роутер TP-Link", "Веб-камера Logitech", "USB-хаб",
            "Зарядное устройство", "Чехол для телефона", "Защитное стекло", "Стилус",
            "Подставка для ноутбука", "Сумка для ноутбука", "Беспроводная зарядка",
            "Смарт-часы", "Фитнес-браслет", "Портативная колонка"
        };

        var descriptions = new[]
        {
            "Высококачественное устройство с отличными характеристиками",
            "Новейшая модель с улучшенной производительностью",
            "Идеально подходит для работы и развлечений",
            "Профессиональное оборудование для требовательных пользователей",
            "Стильный дизайн и надежная конструкция"
        };

        // Generate 10-15 random products
        int productCount = _random.Next(10, 16);

        for (int i = 0; i < productCount; i++)
        {
            var product = new Product
            {
                Name = productNames[_random.Next(productNames.Length)],
                Description = descriptions[_random.Next(descriptions.Length)] +
                             $" | Артикул: {_random.Next(1000, 9999)}"
            };
            products.Add(product);
        }

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
