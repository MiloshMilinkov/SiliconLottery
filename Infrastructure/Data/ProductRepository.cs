using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    //Using a repository we create another level of abastraction away from our data.
    //This can be harmful to our performance sometimes, but what we gain is cleaner API controllers
    // and greater level of testability and maintainability.
    public class ProductRepository : IProductRepository
    {
        //Repo will deal with our DbContext while the API controller will deal with hte interface.
        private readonly StoreContext _storeContext;
        public ProductRepository(StoreContext storeContext){
            _storeContext=storeContext;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
        {
            return await _storeContext.ProductBrands.ToListAsync();
        }

        public async Task<ProductBrand> GetBrandsByIdAsync(int id)
        {
            return await _storeContext.ProductBrands.FindAsync(id);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _storeContext.Products
                .Include(p=>p.ProductType)
                .Include(p=>p.ProductBrand)
                .FirstOrDefaultAsync(p=>p.Id==id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _storeContext.Products
                .Include(p=>p.ProductType)
                .Include(p=>p.ProductBrand)
                .ToListAsync();
        }

        public async Task<(IReadOnlyList<Product>,int)> GetProductsAsync(string searchTerm = null, string orderBy = "nameAsc", int? pageIndex = null, int pageSize = 5, int? typeId = null, int? brandId = null)
        {
            var query = _storeContext.Products.AsQueryable();
            
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm));
            }
            if (typeId.HasValue && typeId>0)
            {
                query = query.Where(p => p.ProductTypeId == typeId.Value);
            }
            
            if (brandId.HasValue && brandId>0)
            {
                query = query.Where(p => p.ProductBrandId == brandId.Value);
            }
            int totalCount = await query.CountAsync();
            
            query = orderBy.ToLower() switch
            {
                "pricedesc" => query.OrderByDescending(p => p.Price),
                "priceasc" => query.OrderBy(p => p.Price),
                "namedesc" => query.OrderByDescending(p => p.Name),
                _ => query.OrderBy(p => p.Name),
            };

             
            if (pageIndex.HasValue)
            {
                query = query.Skip(pageSize * (pageIndex.Value-1)).Take(pageSize);
            }

            var products= await query
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .ToListAsync();
            return(products,totalCount);
        }
        public async Task<ProductType> GetTypeByIdAsync(int id)
        {
            return await _storeContext.ProductTypes.FindAsync(id);
        }
        public async Task<IReadOnlyList<ProductType>> GetTypesAsync()
        {
            return await _storeContext.ProductTypes.ToListAsync();
        }
    }
}