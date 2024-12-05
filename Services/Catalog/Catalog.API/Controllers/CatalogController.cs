using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers;

public class CatalogController(IMediator mediator) : ApiController
{
    [HttpGet]
    [Route("[action]/{id}", Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ProductResponse>> GetProductById(string id)
    {
        var query = new GetProductByIdQuery(id);
        var result = await mediator.Send(query);
        return result is not null ? Ok(result) : NotFound();
    }


    [HttpGet]
    [Route("[action]/{name}", Name = "GetProductByName")]
    [ProducesResponseType(typeof(List<ProductResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<IList<ProductResponse>>> GetProductByName(string name)
    {
        var query = new GetProductByNameQuery(name);
        var result = await mediator.Send(query);
        return result is not null ? Ok(result) : NotFound();
    }


    [HttpGet]
    [Route("[action]", Name = "GetProducts")]
    [ProducesResponseType(typeof(Pagination<ProductResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<Pagination<ProductResponse>>> GetProducts([FromQuery] CatalogSpecParams catalogSpecParams)
    {
        var query = new GetAllProductsQuery(catalogSpecParams);
        var result = await mediator.Send(query);
        return result is not null ? Ok(result) : NotFound();
    }


    [HttpGet]
    [Route("[action]", Name = "GetBrands")]
    [ProducesResponseType(typeof(IList<BrandsResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<IList<BrandsResponse>>> GetBrands()
    {
        var query = new GetAllBrandsQuery();
        var result = await mediator.Send(query);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpGet]
    [Route("[action]", Name = "GetTypes")]
    [ProducesResponseType(typeof(IList<TypeReponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<IList<TypeReponse>>> GetTypes()
    {
        var query = new GetAllTypesQuery();
        var result = await mediator.Send(query);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpGet]
    [Route("[action]/{brandName}", Name = "GetProductByBrand")]
    [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<IList<ProductResponse>>> GetProductByBrand(string brandName)
    {
        var query = new GetProductByBrandQuery(brandName);
        var result = await mediator.Send(query);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    [Route("CreateProduct")]
    [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand command)
    {
        var result = await mediator.Send(command);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPut]
    [Route("UpdateProduct")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<bool>> UpdateProduct([FromBody] UpdateProductCommand command)
    {
        var result = await mediator.Send(command);
        return result ? Ok(result) : NotFound();
    }

    [HttpDelete]
    [Route("[action]/{id}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<bool>> DeleteProduct(string id)
    {
        var command = new DeleteProductCommand(id);
        var result = await mediator.Send(command);
        return result ? Ok(result) : NotFound();
    }

}
