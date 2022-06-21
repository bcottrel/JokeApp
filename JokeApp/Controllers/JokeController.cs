using JokeApp.Model;
using JokeApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JokeApp.Controllers
{
    public class JokeController : Controller
    {
        //GET: JokeController/
        public IActionResult Index()
        {
            JokeDAO jokes = new JokeDAO();

            return View(jokes.GetAllJokes());
        }

        public IActionResult SearchForm()
        {
            return View();
        }

        //Get JokeController/SearchResults/
        public IActionResult SearchResults(string searchTerm)
        {
            JokeDAO jokes = new JokeDAO();
            List<Joke> jokeList = jokes.SearchJokes(searchTerm);

            return View("index", jokeList);
        }

        
        //GET: JokeController/Create
        [Authorize]
        public IActionResult InputForm()
        {
            return View();
        }

        //POST: JokeController/Create/
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessCreate(Joke joke)
        {
            if (ModelState.IsValid) {
                JokeDAO jokes = new JokeDAO();
                jokes.Insert(joke);
                TempData["success"] = "Joke Created";

                return RedirectToAction("Index");
            }
            return View(joke);
        }

        // GET: JokeController/Details/
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            JokeDAO jokes = new JokeDAO();
            Joke foundJoke = await Task.Run(() => jokes.FindId(id));

            if (foundJoke == null)
            {
                return NotFound();
            }

            return View("Details", foundJoke);
        }

        // GET: JokeController/Edit/
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            JokeDAO jokes = new JokeDAO();
            Joke foundJoke = await Task.Run(() => jokes.FindId(id));
           
            if (foundJoke == null)
            {
                return NotFound();
            }

            return View("ShowEdit", foundJoke);
        }

        //Post: JokeController/Edit/
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessEdit(Joke joke)
        {
            JokeDAO jokes = new JokeDAO();
            jokes.Update(joke);
            TempData["success"] = "Joke Edited";
            return RedirectToAction("Index");
        }

        //Get: JokeController/Delete/
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            JokeDAO jokes = new JokeDAO();
            Joke joke = await Task.Run(() => jokes.FindId(id));

            if (joke == null)
            {
                return NotFound();
            }
            return View(joke);
        }

        //Post: JokeController/Delete/
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessDelete(int id)
        {
            JokeDAO jokes = new JokeDAO();
            Joke joke = await Task.Run(() => jokes.FindId(id));
            jokes.Delete(joke);
            TempData["success"] = "Joke Deleted";

            return RedirectToAction("Index");
        }
    }
}
