using Xunit;
using Moq;
using JokeApp.Model;
using JokeApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using JokeApp.Services;

namespace JokeApp.tests
{
    public class HomeControllerTests
    {
        private readonly IConfiguration _configuration;

        HomeControllerTests(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        [Fact]
        public void Index_ReturnsAView()
        {
            // Arrange
            var mock = new Mock<IJokeDataService>()
                .Setup(x => x.GetAllJokes(_configuration))
                .Returns(GetSampleJokes());
            var controller = new JokeController(_configuration);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Joke>>(
                viewResult.ViewData.Model);
            Assert.Equal(4, model.Count());
        }

         

        private List<Joke> GetSampleJokes()
        {
            List<Joke> jokes = new List<Joke>()
            {

			    new Joke
			    {
				    JokeQuestion = "The",
				    JokeAnswer = "Frisbe"
			    },
			    new Joke
			    {
				    JokeQuestion = "The",
				    JokeAnswer = "Frisbe"
			    },
			    new Joke
			    {
				    JokeQuestion = "The",
				    JokeAnswer = "Frisbe"
			    }

            };

            return jokes;
        }
    }
}