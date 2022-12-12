using JokeApp.Models;
using JokeApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JokeApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IJokeDataService _jokeData; 

    public HomeController(ILogger<HomeController> logger, IJokeDataService jokeData)
    {
        _jokeData = jokeData;
        _logger = logger;
    }

    public IActionResult Index()
    {
        Joke joke = _jokeData.RandomJoke();
        return View(joke);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}