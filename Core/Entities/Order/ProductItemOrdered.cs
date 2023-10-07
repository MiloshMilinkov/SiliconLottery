using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Order
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
        }
        public ProductItemOrdered(int productItemId, string productname, string pictureUrl)
        {
            ProductItemId = productItemId;
            ProductName = productname;
            PictureUrl = pictureUrl;
        }

        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}