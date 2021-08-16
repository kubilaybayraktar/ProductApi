using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Filters;
using Product.Cache;
using System.Collections.Generic;

namespace Product.Api.Controllers
{
    [Produces("application/json")]
    [Route("categories")]
    [ApiController]
    public class CategoriesController : Controller
    {
        #region Props
        private readonly CategoryHelper _categoryHelper;
        private readonly ICacheService _cacheService;
        #endregion

        #region Ctor
        public CategoriesController(CategoryHelper categoryHelper, ICacheService cacheService)
        {
            _categoryHelper = categoryHelper;
            _cacheService = cacheService;
        }
        #endregion

        #region Get

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>Categories</returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductModel>))]
        [ServiceFilter(typeof(TimerAction))]
        public IActionResult GetAll([FromQuery] StatusQueryModel model)
        {
            List<LookupCategoryModel> categories = _cacheService.GetAllCategories(model.Status);

            return Ok(categories);
        }

        /// <summary>
        /// Get categories by name
        /// </summary>
        /// <returns>Categories</returns>
        [HttpGet("name")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductModel))]
        public IActionResult GetByName([FromQuery] NameQueryModel model)
        {
            LookupCategoryModel category = _categoryHelper.GetCategoryByName(model.Name);

            return Ok(category);
        }
        #endregion

        #region Add

        /// <summary>
        /// Add new category
        /// </summary>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(ResetCache))]
        public IActionResult AddCategory([FromBody] AddCategoryBodyModel model)
        {
            LookupCategoryModel category = _categoryHelper.AddCategory(model);

            return Ok(category);
        }
        #endregion

        #region Update

        /// <summary>
        /// Update category
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<LookupCategory>))]
        [ServiceFilter(typeof(ResetCache))]
        public IActionResult UpdateCategory(int id, [FromBody] UpdateCategoryBodyModel model)
        {
            _categoryHelper.UpdateCategory(id, model.Name);

            return Ok();
        }
        #endregion

        #region Category Attributes

        /// <summary>
        /// Add attribute to category
        /// </summary>
        [HttpPost("{id}/attribute/{attributeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<LookupCategory>))]
        [ServiceFilter(typeof(ResetCache))]
        public IActionResult AddAttribute(int id, int attributeId)
        {
            _categoryHelper.AddAttribute(id, attributeId);

            return Ok();
        }

        /// <summary>
        /// Remove attribute from category
        /// </summary>
        [HttpDelete("{id}/attribute/{attributeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<LookupCategory>))]
        [ServiceFilter(typeof(ResetCache))]
        public IActionResult RemoveAttribute(int id, int attributeId)
        {
            _categoryHelper.RemoveAttribute(id, attributeId);

            return Ok();
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete category
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<LookupCategory>))]
        [ServiceFilter(typeof(ResetCache))]
        public IActionResult DeleteCategory(int id)
        {
            _categoryHelper.DeleteCategory(id);

            return Ok();
        }
        #endregion

        #region Attributes

        #region Get

        /// <summary>
        /// Get all attributes
        /// </summary>
        [HttpGet("attributes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LookupAttributeModel>))]
        public IActionResult GetAllAttributes()
        {
            List<LookupAttributeModel> attributes = _categoryHelper.GetAllAttributes();

            return Ok(attributes);
        }
        #endregion

        #region Add

        /// <summary>
        /// Add new attribute
        /// </summary>
        [HttpPost("attributes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LookupAttributeModel))]
        public IActionResult AddAttribute([FromBody] NameQueryModel model)
        {
            LookupAttributeModel attribute = _categoryHelper.AddAttribute(model.Name);

            return Ok(attribute);
        }
        #endregion

        #region Update

        /// <summary>
        /// Update attribute
        /// </summary>
        [HttpPut("attributes/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LookupAttributeModel))]
        public IActionResult UpdateAttribute(int id, [FromBody] NameQueryModel model)
        {
            _categoryHelper.UpdateAttribute(id, model.Name);

            return Ok();
        }
        #endregion

        #region Delete

        /// <summary>
        /// Delete attribute and relations
        /// </summary>
        [HttpDelete("attributes/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteAttribute(int id)
        {
            _categoryHelper.DeleteAttribute(id);

            return Ok();
        }
        #endregion

        #endregion
    }
}
