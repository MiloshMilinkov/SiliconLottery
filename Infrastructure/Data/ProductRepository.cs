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
            return await _storeContext.Products.FindAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _storeContext.Products.ToListAsync();
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