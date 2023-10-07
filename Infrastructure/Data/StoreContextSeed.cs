using System.Text.Json;
using Core.Entities;
using Core.Entities.Order;
namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext storeContext)
        {
            if(!storeContext.ProductBrands.Any()){
                var brandsJson=File.ReadAllText("../Infrastructure/Data/SeedData/Brands.json");
                var brands=JsonSerializer.Deserialize<List<ProductBrand>>(brandsJson);
                storeContext.ProductBrands.AddRange(brands);
            }
            if(!storeContext.ProductTypes.Any()){
                var typesJson=File.ReadAllText("../Infrastructure/Data/SeedData/Types.json");
                var types=JsonSerializer.Deserialize<List<ProductType>>(typesJson);
                storeContext.ProductTypes.AddRange(types);
            }
            if(!storeContext.Products.Any()){
                var productsJson=File.ReadAllText("../Infrastructure/Data/SeedData/Products.json");
                var products=JsonSerializer.Deserialize<List<Product>>(productsJson);
                storeContext.Products.AddRange(products);
            }
            if(!storeContext.Delivery.Any()){
                var deliveryJson=File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");
                var deliveries=JsonSerializer.Deserialize<List<Delivery>>(deliveryJson);
                storeContext.Delivery.AddRange(deliveries);
            }
            
            if(storeContext.ChangeTracker.HasChanges()){
                await storeContext.SaveChangesAsync();
            }
        }
    }
}