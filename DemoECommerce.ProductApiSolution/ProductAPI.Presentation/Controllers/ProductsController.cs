using eCommerce.SharedLibrary.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.DTOs;
using ProductApi.Application.DTOs.Conversions;
using ProductApi.Application.Interfaces;

namespace ProductAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductsController (IProduct productInterface): ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {

            // Get all product from repo
            var products=await productInterface.GetAllAsync();
            if (!products.Any())
                return NotFound("No products detected in the database");


            //convert data from entity to DTO and return 
            var (_, list) = ProductConversions.FromEntity(null!, products);
            return list!.Any() ? Ok(list) : NotFound("No product  found");

        }

        [HttpGet ("{id:int}")]
        public async Task<ActionResult<ProductDTO>>GetProduct(int id)
        {

            // Get product from the Repo
            var product = await productInterface.FindByIdAsync(id);
            if (product is null)
                return NotFound("Product  request not found ");

            //Convert from entity  toDto  and return 

            var (_product, _) = ProductConversions.FromEntity(product, null!);
            return _product is not null ? Ok(_product) : NotFound("Product not found ");

           
        }


        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<Response>> CreateProduct(ProductDTO product)
        {
            //Check model state is all data annotations are passed
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //Convert to entity
            var getEntity = ProductConversions.ToEntity(product);
            var response = await productInterface.CreateAsync(getEntity);
            return response.Flag is true ? Ok(response):BadRequest(response);


        }


        [HttpPut]
        [Authorize(Roles ="Admin")]
        public async  Task<ActionResult<Response>> UpdateProduct(ProductDTO product)
        {
            //Check the model  state is  all the data annotation are passed
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Convert  to entity
            var getEntity=ProductConversions.ToEntity(product);
            var response = await productInterface.UpdateAsync(getEntity);
            return response.Flag is true ? Ok(response) : BadRequest(response);


        }
        [HttpDelete]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<Response>> DeleteProduct(ProductDTO product)
        {
            // convert to entity
            var getEntity = ProductConversions.ToEntity(product);
            var response = await productInterface.DeleteAsync(getEntity);
            return response.Flag is true ? Ok(response) : BadRequest(response);

           
        }
    }

}
