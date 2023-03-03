namespace Core.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }      
        public string Description { get; set; }        
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        //ProductType and ProductBrand will help EntityFram to better migrate data 
        //and auto create relations bassed on each Id
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }

        public ProductBrand ProductBrand{ get; set; }
        public int ProductBrandId { get; set; }
    }
}