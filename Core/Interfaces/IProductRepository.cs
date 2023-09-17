using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository                                                                                                                                                           
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<(IReadOnlyList<Product>, int)> GetProductsAsync(string searchTerm = null, string orderBy = "nameAsc", 
                                                             int? pageIndex = null,int pageSize = 5, 
                                                             int? typeId = null, int? brandId = null);
        Task<ProductBrand> GetBrandsByIdAsync(int id);
        Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();
        Task<ProductType> GetTypeByIdAsync(int id);
        Task<IReadOnlyList<ProductType>> GetTypesAsync();
    }
}