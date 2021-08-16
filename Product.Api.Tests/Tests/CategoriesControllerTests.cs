using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Product.Api.Tests
{
    public class CategoriesControllerTests : TestBase, IClassFixture<TestFixture<Startup>>
    {
        private readonly HttpClient Client;
        private readonly string Path = "/categories";
        public CategoriesControllerTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        /// <summary>
        /// Get active & passive categories valid call
        /// </summary>
        /// <param name="status">Category status</param>
        /// <returns></returns>
        [Theory]
        [Trait("Api", "Categories")]
        [InlineData(1)]
        [InlineData(0)]
        public async Task Get_Categories_Valid_Call(short status)
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
        /// Get categories by name valid call
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Api", "Categories")]
        public async Task Get_Categories_By_Name_Valid_Call()
        {
            // Arrange
            var category = GetRandomCategory();
            var request = new
            {
                Url = $"{Path}/name?name={category.Name}"
            };

            // Act
            var response = await Client.GetAsync(request.Url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            LookupCategory returnCategory = JsonConvert.DeserializeObject<LookupCategory>(await response.Content.ReadAsStringAsync());
            Assert.Equal(category.Id, returnCategory.Id);
        }

        /// <summary>
        /// Get categories by name valid call
        /// </summary>
        /// <returns></returns>
        [Theory]
        [Trait("Api", "Categories")]
        [InlineData("%+%&")]
        public async Task Get_Categories_By_Name_InValid_Call(string name)
        {
            // Arrange
            var request = new
            {
                Url = $"{Path}/name?name={name}"
            };

            // Act
            var response = await Client.GetAsync(request.Url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Add category
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Api", "Categories")]
        public async Task Add_Category()
        {
            // Arrange
            int attributeId = GetRandomAttributeId();
            var model = ModelCreator.GetAddCategoryBodyModel(new() { attributeId });
            var request = new
            {
                Url = $"{Path}",
                Body = model
            };

            // Act
            var response = await Client.PostAsync(request.Url, GetStringContent(request.Body));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            LookupCategory returnCategory = JsonConvert.DeserializeObject<LookupCategory>(await response.Content.ReadAsStringAsync());
            Assert.True(returnCategory.Id > 0);
        }

        /// <summary>
        /// Add category invalid attribute id
        /// </summary>
        /// <returns></returns>
        [Theory]
        [Trait("Api", "Categories")]
        [InlineData(0)]
        [InlineData(-50)]
        public async Task Add_Category_Invalid_Attribute_Id(int attributeId)
        {
            // Arrange
            var model = ModelCreator.GetAddCategoryBodyModel(new() { attributeId });
            var request = new
            {
                Url = $"{Path}",
                Body = model
            };

            // Act
            var response = await Client.PostAsync(request.Url, GetStringContent(request.Body));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        /// <summary>
        /// Update Category
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Api", "Categories")]
        public async Task Update_Category()
        {
            // Arrange
            var category = GetRandomCategory();
            var model = ModelCreator.GetUpdateCategoryBodyModel();
            var request = new
            {
                Url = $"{Path}/{category.Id}",
                Body = model
            };

            // Act
            var response = await Client.PutAsync(request.Url, GetStringContent(request.Body));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        /// <summary>
        /// Update Category invalid name
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Api", "Categories")]
        public async Task Update_Category_Invalid_Name()
        {
            // Arrange
            var category = GetRandomCategory();
            var request = new
            {
                Url = $"{Path}/{category.Id}",
                Body = ""
            };

            // Act
            var response = await Client.PutAsync(request.Url, GetStringContent(request.Body));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Add invalid attribute to category
        /// </summary>
        /// <returns></returns>
        [Theory]
        [Trait("Api", "Categories")]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        public async Task Add_Invalid_Attribute_To_Category(int attributeId)
        {
            // Arrange
            var category = GetRandomCategory();
            var request = new
            {
                Url = $"{Path}/{category.Id}/attribute/{attributeId}"
            };

            // Act
            var response = await Client.PostAsync(request.Url, null);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Remove invalid attribute from category
        /// </summary>
        /// <returns></returns>
        [Theory]
        [Trait("Api", "Categories")]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        public async Task Remove_Invalid_Attribute_From_Category(int attributeId)
        {
            // Arrange
            var category = GetRandomCategory();
            var request = new
            {
                Url = $"{Path}/{category.Id}/attribute/{attributeId}"
            };

            // Act
            var response = await Client.DeleteAsync(request.Url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Get all attributes
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Api", "Categories")]
        public async Task Get_Attributes_Valid_Call()
        {
            // Arrange
            var request = new
            {
                Url = $"{Path}/attributes"
            };

            // Act
            var response = await Client.GetAsync(request.Url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        /// <summary>
        /// Add attribute
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Api", "Categories")]
        public async Task Add_Attribute()
        {
            // Arrange
            var request = new
            {
                Url = $"{Path}/attributes",
                Body = new NameQueryModel() { Name = ModelCreator.GetRandomAttributeName }
            };

            // Act
            var response = await Client.PostAsync(request.Url, GetStringContent(request.Body));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            LookupAttributeModel returnAttribute = JsonConvert.DeserializeObject<LookupAttributeModel>(await response.Content.ReadAsStringAsync());
            Assert.True(returnAttribute.Id > 0);
        }

        /// <summary>
        /// Add attribute invalid name
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Api", "Categories")]
        public async Task Add_Attribute_Invalid_Name()
        {
            // Arrange
            var request = new
            {
                Url = $"{Path}/attributes",
                Body = new NameQueryModel() { Name = "" }
            };

            // Act
            var response = await Client.PostAsync(request.Url, GetStringContent(request.Body));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        /// <summary>
        /// Update attribute
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Api", "Categories")]
        public async Task Update_Attribute()
        {
            // Arrange
            int attributeId = GetRandomAttributeId();
            var request = new
            {
                Url = $"{Path}/attributes/{attributeId}",
                Body = new NameQueryModel() { Name = ModelCreator.GetRandomAttributeName }
            };

            // Act
            var response = await Client.PutAsync(request.Url, GetStringContent(request.Body));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        /// <summary>
        /// Update attribute invalid attribute id
        /// </summary>
        /// <returns></returns>
        [Theory]
        [Trait("Api", "Categories")]
        [InlineData(0)]
        [InlineData(-5285)]
        public async Task Update_Attribute_Invalid_Attribute_Id(int attributeId)
        {
            // Arrange
            var request = new
            {
                Url = $"{Path}/attributes/{attributeId}",
                Body = new NameQueryModel() { Name = ModelCreator.GetRandomAttributeName }
            };

            // Act
            var response = await Client.PutAsync(request.Url, GetStringContent(request.Body));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}
