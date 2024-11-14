using ProductApi.Domain.Entities;
namespace ProductApi.Application.DTOs.Conversions
{
    public  static class ProductConversions
    {

        //entity we pass in this product dto that we created as product and  here we are going to use 
        public static Product ToEntity(ProductDTO product) => new()
        {
            Id= product.Id,
            Name= product.Name,
            Quantity=product.Quantity,
            Price=product.Price

        };
        // we have  this return type so it must take in the same as a playload
        // to make request to database 
        public static (ProductDTO?,IEnumerable<ProductDTO>?) FromEntity(Product product ,IEnumerable<Product>? products)
        {
            //return single
            if(product is not null || products is null)
            {
                var singleProduct = new ProductDTO
                    (product!.Id,
                    product.Name!,
                    product.Quantity,
                    product.Price
                    );
                return (singleProduct, null);
            }
            // return list
             if (products is not null || products is null)
            {
                var _products = products!.Select(p =>
                   new ProductDTO(p.Id, p.Name!, p.Quantity, p.Price)).ToList();
                return (null,_products);
            }
            return (null, null);

        }


    }
}
