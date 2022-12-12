using Xunit;
using Moq;
using JokeApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using JokeApp.Services;
using JokeApp.Models;
using Autofac.Extras.Moq;

namespace JokeApp.tests;

public class HomeControllerTests
{
    [Fact]
    public void Index_ReturnsAView()
    {
        // Arrange
        var mock = new Mock<IJokeDataService>();
            mock.Setup(x => x.GetAllJokes())
            .Returns(GetSampleJokes());
        var controller = new JokeController(mock.Object);

        // Act
        var result = controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Joke>>(
            viewResult.ViewData.Model);
        Assert.Equal(3, model.Count());
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