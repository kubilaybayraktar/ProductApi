using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Filters;
using Product.Cache;
using System.Collections.Generic;

namespace Product.Api.Controllers
{
    [Produces("application/json")]
    [Route("products")]
    [ApiController]
    public class ProductsController : Controller
    {
        #region Props
        private readonly ProductHelper _productHelper;
        private readonly ICacheService _cacheService;
        #endregion

        #region Ctor
        public ProductsController(ProductHelper productHelper, ICacheService cacheService)
        {
            _productHelper = productHelper;
            _cacheService = cacheService;
        }
        #endregion

        #region Get

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>Products</returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductModel>))]
        [ServiceFilter(typeof(TimerAction))]
        public IActionResult GetAll([FromQuery] StatusQueryModel model)
        {
            List<ProductModel> products = _cacheService.GetAllProducts(model.Status);

            return Ok(products);
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Product</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            ProductModel product = _productHelper.GetProductById(id);

            return Ok(product);
        }

        /// <summary>
        /// Get product by name
        /// </summary>
        /// <param name="model">Product name</param>
        /// <returns>Product</returns>
        [HttpGet("name")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByName([FromQuery] NameQueryModel model)
        {
            ProductModel product = _productHelper.GetProductByName(model.Name);

            return Ok(product);
        }


        /// <summary>
        /// Get products by category name
        /// </summary>
        /// <param name="model">Category name</param>
        /// <returns>Products</returns>
        [HttpGet("categoryName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductModel>))]
        public IActionResult GetByCategoryName([FromQuery] NameQueryModel model)
        {
            List<ProductModel> products = _productHelper.GetProductsByCategoryName(model.Name);

            return Ok(products);
        }

        /// <summary>
        /// Get products between prices
        /// </summary>
        /// <param name="model">Price range</param>
        /// <returns>Products</returns>
        [HttpGet("priceRange")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByPriceRange([FromQuery] PriceRangeQueryModel model)
        {
            List<ProductModel> products = _productHelper.GetProductsByPriceRange(model.Min, model.Max);

            return Ok(products);
        }
        #endregion

        #region Add

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="model">Add product model</param>
        /// <returns>Product</returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductModel))]
        [ServiceFilter(typeof(ResetCache))]
        public IActionResult AddProduct([FromBody] ProductBodyModel model)
        {
            ProductModel product = _productHelper.AddProduct(model);

            return Ok(product);
        }
        #endregion

        #region Update

        /// <summary>
        /// Update product by id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="model">Update product model</param>
        /// <returns>Ok</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(ResetCache))]
        public IActionResult UpdateProduct(int id, [FromBody] UpdateProductBodyModel model)
        {
            _productHelper.UpdateProduct(id, model);

            return Ok();
        }
        #endregion

        #region Delete

        /// <summary>
        /// Delete product by id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Ok</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(ResetCache))]
        public IActionResult DeleteProduct(int id)
        {
            _productHelper.DeleteProduct(id);

            return Ok();
        }
        #endregion
    }
}
