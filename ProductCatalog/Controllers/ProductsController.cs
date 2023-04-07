using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Products.Commands;
using ProductCatalog.Application.Products.Commands.UpdateProduct;
using ProductCatalog.Models;
using ProductCatalog.Resources.Commands.Create;
using ProductCatalog.Resources.Notofications;
using ProductCatalog.Resources.Queries;

namespace ProductCatalog.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISender _sender;
        public ProductsController(IMediator mediator, ISender sender) 
        {
            _mediator = mediator;
            _sender = sender;
        }

        [HttpGet]
        //wikame w postman s api/products get metghod
        public async Task<ActionResult> GetProducts()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpPost]
        //wikame w postman s api/products post method i podawame obekt bez id !!!! pri  add
        //wikame w postman s api/products post method i podawame obekt s id !!!! pri  modify 
        public async Task<ActionResult> AddProduct([FromBody] Product product)
        {
            //pri add samo
            //await _mediator.Send(new CreateProductCommand(product));
            //return StatusCode(201);

            var productToReturn = await _mediator.Send(new CreateProductCommand(product));

           // await _mediator.Publish(new ProductAddedNotification(productToReturn));

            return CreatedAtRoute("GetProductById", new { id = productToReturn.Id }, productToReturn);
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        //wikame w postman s api/products/1 get method 
        public async Task<ActionResult> GetProductById(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(product);
        }



        [HttpPut("{Id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct(int Id, [FromBody] UpdateProductRequest request,
                                                    CancellationToken cancellationToken)
    {
        var command = request.Adapt<UpdateProductCommand>() with
        {
            Id = Id
        };
        await _sender.Send(command, cancellationToken);
        return NoContent();
    }
    }
}
