using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Product.Api.Tests
{
    public class ProductsControllerTests : TestBase, IClassFixture<TestFixture<Startup>>
    {
        private readonly HttpClient Client;
        private readonly string Path = "/products";
        public ProductsControllerTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        /// <summary>
        /// Get active & passive products valid call
        /// </summary>
        /// <param name="status">Product status</param>
        /// <returns></returns>
        [Theory]
        [Trait("Api", "Products")]
        [InlineData(1)]
        [InlineData(0)]
        public async Task Get_Products_Valid_Call(short status)
        {
            // Arrange
            var request = new
            {
                Url = $"{Path}?status={status}"
            };

            // Act
            var response = await Client.GetAsync(request.Url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        /// <summary>
        /// Get product by id valid call
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Api", "Products")]
        public async Task Get_Product_By_Id_Valid_Call()
        {
            // Arrange
            Product product = GetRandomProduct();

            var request = new
            {
                Url = $"{Path}/{product.Id}"
            };

            // Act
            var response = await Client.GetAsync(request.Url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Product returnProduct = JsonConvert.DeserializeObject<Product>(await response.Content.ReadAsStringAsync());
            Assert.Equal(product.Id, returnProduct.Id);
        }

        /// <summary>
        /// Get product by invalid id
        /// </summary>
        /// <returns></returns>
        [Theory]
        [Trait("Api", "Products")]
        [InlineData(0)]
        [InlineData(-25)]
        public async Task Get_Product_By_Id_InValid_Call(int id)
        {
            // Arrange
            var request = new
            {
                Url = $"{Path}/{id}"
            };

            // Act
            var response = await Client.GetAsync(request.Url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


        /// <summary>
        /// Get product by name valid call
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Api", "Products")]
        public async Task Get_Product_By_Name_Valid_Call()
        {
            // Arrange
            Product product = GetRandomProduct();

            var request = new
            {
                Url = $"{Path}/name?name={product.Name}"
            };

            // Act
            var response = await Client.GetAsync(request.Url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Product returnProduct = JsonConvert.DeserializeObject<Product>(await response.Content.ReadAsStringAsync());
            Assert.Equal(product.Id, returnProduct.Id);
        }

        /// <summary>
        /// Get product by invalid name
        /// </summary>
        /// <returns></returns>
        [Theory]
        [Trait("Api", "Products")]
        [InlineData("!?+")]
        [InlineData("XXX")]
        public async Task Get_Product_By_Name_InValid_Call(string name)
        {
            // Arrange
            var request = new
            {
                Url = $"{Path}/name?name={name}"
            };

            // Act
            var response = await Client.GetAsync(request.Url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Get product by category name valid call
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Api", "Products")]
        public async Task Get_Product_By_Category_Name_Valid_Call()
        {
            // Arrange
            LookupCategory category = GetRandomCategory();

            var request = new
            {
                Url = $"{Path}/categoryName?name={category.Name}"
            };

            // Act
            var response = await Client.GetAsync(request.Url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        /// <summary>
        /// Get product by invalid category name
        /// </summary>
        /// <returns></returns>
        [Theory]
        [Trait("Api", "Products")]
        [InlineData("!?+")]
        [InlineData("XXX")]
        public async Task Get_Product_By_Category_Name_InValid_Call(string name)
        {
            // Arrange
            var request = new
            {
                Url = $"{Path}/categoryName?name={name}"
            };

            // Act
            var response = await Client.GetAsync(request.Url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Get products by price range valid call
        /// </summary>
        /// <returns></returns>
        [Theory]
        [Trait("Api", "Products")]
        [InlineData(1, int.MaxValue)]
        public async Task Get_Products_By_Price_Range_Valid_Call(int min, int max)
        {
            // Arrange
            var request = new
            {
                Url = $"{Path}/priceRange?min={min}&max={max}"
            };

            // Act
            var response = await Client.GetAsync(request.Url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            List<Product> returnProducts = JsonConvert.DeserializeObject<List<Product>>(await response.Content.ReadAsStringAsync());
            Assert.True(returnProducts.Any());
        }

        /// <summary>
        /// Get products by price range invalid call
        /// </summary>
        /// <returns></returns>
        [Theory]
        [Trait("Api", "Products")]
        [InlineData(0, int.MaxValue)]
        [InlineData(1, -5)]
        public async Task Get_Products_By_Price_Range_InValid_Call(int min, int max)
        {
            // Arrange
            var request = new
            {
                Url = $"{Path}/priceRange?min={min}&max={max}"
            };

            // Act
            var response = await Client.GetAsync(request.Url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        /// <summary>
        /// Add product
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Api", "Products")]
        public async Task Add_Product()
        {
            // Arrange
            int categoryId = GetRandomCategory().Id;
            int attributeId = GetRandomAttributeIdByCategoryId(categoryId);
            var model = ModelCreator.GetProductBodyModel(categoryId, new() { attributeId });
            var request = new
            {
                Url = $"{Path}",
                Body = model
            };

            // Act
            var response = await Client.PostAsync(request.Url, GetStringContent(request.Body));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Product returnProduct = JsonConvert.DeserializeObject<Product>(await response.Content.ReadAsStringAsync());
            Assert.True(returnProduct.Id > 0);
        }

        /// <summary>
        /// Add product invalid categoryId
        /// </summary>
        /// <returns></returns>
        [Theory]
        [Trait("Api", "Products")]
        [InlineData(-5)]
        public async Task Add_Product_Invalid_CategoryId(int categoryId)
        {
            // Arrange
            int validCategoryId = GetRandomCategory().Id;
            int attributeId = GetRandomAttributeIdByCategoryId(validCategoryId);
            var model = ModelCreator.GetProductBodyModel(categoryId, new() { attributeId });
            var request = new
            {
                Url = $"{Path}",
                Body = model
            };

            // Act
            var response = await Client.PostAsync(request.Url, GetStringContent(request.Body));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Add product invalid attributeId
        /// </summary>
        /// <returns></returns>
        [Theory]
        [Trait("Api", "Products")]
        [InlineData(-5)]
        public async Task Add_Product_Invalid_AttributeId(int attributeId)
        {
            // Arrange
            int categoryId = GetRandomCategory().Id;
            var model = ModelCreator.GetProductBodyModel(categoryId, new() { attributeId });
            var request = new
            {
                Url = $"{Path}",
                Body = model
            };

            // Act
            var response = await Client.PostAsync(request.Url, GetStringContent(request.Body));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Api", "Products")]
        public async Task Update_Product()
        {
            // Arrange
            var product = GetRandomProduct();
            int attributeId = GetRandomAttributeIdByCategoryId(product.CategoryId);
            var model = ModelCreator.GetUpdateProductBodyModel(new() { attributeId });
            var request = new
            {
                Url = $"{Path}/{product.Id}",
                Body = model
            };

            // Act
            var response = await Client.PutAsync(request.Url, GetStringContent(request.Body));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        /// <summary>
        /// Update product invalid attribute id
        /// </summary>
        /// <returns></returns>
        [Theory]
        [Trait("Api", "Products")]
        [InlineData(-5)]
        public async Task Update_Product_Invalid_Attribute_Id(int attributeId)
        {
            // Arrange
            var product = GetRandomProduct();
            var model = ModelCreator.GetUpdateProductBodyModel(new() { attributeId });
            var request = new
            {
                Url = $"{Path}/{product.Id}",
                Body = model
            };

            // Act
            var response = await Client.PutAsync(request.Url, GetStringContent(request.Body));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Update product invalid name
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Api", "Products")]
        public async Task Update_Product_Invalid_Name()
        {
            // Arrange
            var product = GetRandomProduct();
            int attributeId = GetRandomAttributeIdByCategoryId(product.CategoryId);
            var model = ModelCreator.GetUpdateProductBodyModel(new() { attributeId }, "", 10);
            var request = new
            {
                Url = $"{Path}/{product.Id}",
                Body = model
            };

            // Act
            var response = await Client.PutAsync(request.Url, GetStringContent(request.Body));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        /// <summary>
        /// Update product invalid price
        /// </summary>
        /// <returns></returns>
        [Theory]
        [Trait("Api", "Products")]
        [InlineData(0)]
        public async Task Update_Product_Invalid_Price(int price)
        {
            // Arrange
            var product = GetRandomProduct();
            int attributeId = GetRandomAttributeIdByCategoryId(product.CategoryId);
            var model = ModelCreator.GetUpdateProductBodyModel(new() { attributeId }, price);
            var request = new
            {
                Url = $"{Path}/{product.Id}",
                Body = model
            };

            // Act
            var response = await Client.PutAsync(request.Url, GetStringContent(request.Body));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

    }
}
