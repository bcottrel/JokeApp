using JokeApp.Models;
using JokeApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace JokeApp.Controllers;

public class JokeController : Controller
{    
    private readonly IJokeDataService _jokeData;

    public JokeController(IJokeDataService jokeData)
    {
        _jokeData = jokeData;
    }

    //GET: JokeController/
    public IActionResult Index()
    {
        return View(_jokeData.GetAllJokes());
    }
    
    public IActionResult SearchForm()
    {
        return View();
    }

    //Get Joke/SearchResults/
    public IActionResult SearchResults(string searchTerm)
    {
        List<Joke> jokeList = _jokeData.SearchJokes(searchTerm);

        return View("index", jokeList);
    }

    
    //GET: Joke/Create
    [Authorize]
    public IActionResult InputForm()
    {
        return View();
    }

    //POST: Joke/Create/
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ProcessCreate([Bind("Id,JokeQuestion,JokeAnswer")]Joke joke)
    {
        if (ModelState.IsValid) {
            _jokeData.Insert(joke);
            TempData["success"] = "Joke Created";

            return RedirectToAction("Index");
        }
        return View(joke);
    }

    // GET: Joke/Details/
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Joke foundJoke = await Task.Run(() => _jokeData.FindId(id));

        if (foundJoke == null)
        {
            return NotFound();
        }

        return View("Details", foundJoke);
    }

    // GET: Joke/Edit/
    [Authorize]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Joke foundJoke = await Task.Run(() => _jokeData.FindId(id));
       
        if (foundJoke == null)
        {
            return NotFound();
        }

        return View("ShowEdit", foundJoke);
    }

    //Post: Joke/Edit/
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ProcessEdit([Bind("Id,JokeQuestion,JokeAnswer")]Joke joke)
    {
        _jokeData.Update(joke);
        TempData["success"] = "Joke Edited";
        return RedirectToAction("Index");
    }

    //Get: Joke/Delete/
    [Authorize]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Joke joke = await Task.Run(() => _jokeData.FindId(id));

        if (joke == null)
        {
            return NotFound();
        }
        return View(joke);
    }

    //Post: Joke/Delete/
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProcessDelete(int id)
    {
        Joke joke = await Task.Run(() => _jokeData.FindId(id));
        _jokeData.Delete(joke);
        TempData["success"] = "Joke Deleted";

        return RedirectToAction("Index");
    }
}
