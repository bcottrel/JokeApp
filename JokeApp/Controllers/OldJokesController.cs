using Microsoft.AspNetCore.Mvc;
using JokeApp.Models;
using JokeApp.Services.Data;
using Microsoft.AspNetCore.Authorization;

namespace JokeApp.Controllers;

public class OldJokesController : Controller
{
    // GET: Joke
    public async Task<IActionResult> Index(IJokeData data)
    {
          return data.GetJokes != null ? 
                      View(await data.GetJokes()) :
                      Problem("There are no jokes.");
    }

    // GET: Joke/Details/
    public async Task<IActionResult> Details(IJokeData data, int id)
    {
        if (data.GetJoke(id) == null)
        {
            return NotFound();
        }

        var joke = await data.GetJoke(id);

        if (joke == null)
        {
            return NotFound();
        }

        return View("Details", joke);
    }

    // GET: Joke/Create
    [Authorize]
    public IActionResult ProcessCreate()
    {
        return View();
    }

    // POST: Joke/Create
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(IJokeData data, [Bind("Id,JokeQuestion,JokeAnswer")] Joke joke)
    {
        if (ModelState.IsValid)
        {
            await data.InsertJoke(joke);
            TempData["success"] = "Joke Created";
            return RedirectToAction(nameof(Index));
        }
        return View(joke);
    }

    // GET: Joke/Edit/5
    [Authorize]
    public async Task<IActionResult> Edit(IJokeData data ,int id)
    {
        var joke = await data.GetJoke(id);

        if (joke == null)
        {
            return NotFound();
        }

        return View("ShowEdit", joke);
    }

    // POST: Joke/Edit/5
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(IJokeData data, [Bind("Id,JokeQuestion,JokeAnswer")]Joke joke)
    {

        if (ModelState.IsValid)
        {
            await data.UpdateJoke(joke);
            TempData["success"] = "Joke Edited";

            return RedirectToAction(nameof(Index));
        }
        return View(joke);
    }

    // GET: Joke/Delete/5
    [Authorize]
    public async Task<IActionResult> Delete(IJokeData data, int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var joke = await data.GetJoke(id);

        if (joke == null)
        {
            return NotFound();
        }

        return View(joke);
    }

    // POST: Joke/Delete/5
    [Authorize]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProcessDelete(IJokeData data,int id)
    {
        await data.DeleteJoke(id);
        TempData["success"] = "Joke Deleted";
        
        return RedirectToAction(nameof(Index));
    }

    // Get Joke/SearchResults
}
