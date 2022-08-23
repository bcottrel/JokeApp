using JokeApp.Models;
using JokeApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JokeApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration; 

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public IActionResult Index()
    {
        JokeDAO jokes = new JokeDAO();
        Joke joke = jokes.RandomJoke(_configuration);
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