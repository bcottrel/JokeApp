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

    [Fact]
    public void SearchResults_ReturnSearchTerm()
    {
        // Arrange
        string s = "Frisbe";
        var mock = new Mock<IJokeDataService>();
        mock.Setup(x => x.SearchJokes(s))
        .Returns(GetSampleJokes());
        var controller = new JokeController(mock.Object);

        //Act
        var result = controller.SearchResults(s);

        //Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var returnValue = Assert.IsType<List<Joke>>(viewResult.Model);
        var idea = returnValue.FirstOrDefault();
        Assert.Equal("Frisbe" , idea.JokeAnswer);

    }

    private List<Joke> GetSampleJokes()
    {
        List<Joke> jokes = new List<Joke>()
        {

            new Joke
            {
                Id = 0,
                JokeQuestion = "The",
                JokeAnswer = "Frisbe"
            },
            new Joke
            {
                Id = 1,
                JokeQuestion = "Went",
                JokeAnswer = "Over"
            },
            new Joke
            {
                Id = 2,
                JokeQuestion = "The",
                JokeAnswer = "Rainbow"
            }

        };

        return jokes;
    }
}