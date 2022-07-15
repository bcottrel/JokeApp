using JokeApp.Model;
using JokeApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace JokeApp.Controllers
{
    public class JokeController : Controller
    {    
        private readonly IConfiguration _configuration;

        public JokeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //GET: JokeController/
        public IActionResult Index()
        {
            JokeDAO jokes = new JokeDAO();

            return View(jokes.GetAllJokes(_configuration));
        }
        
        public IActionResult SearchForm()
        {
            return View();
        }

        //Get Joke/SearchResults/
        public IActionResult SearchResults(string searchTerm)
        {
            JokeDAO jokes = new JokeDAO();
            List<Joke> jokeList = jokes.SearchJokes(searchTerm, _configuration);

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
                JokeDAO jokes = new JokeDAO();
                jokes.Insert(joke, _configuration);
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

            JokeDAO jokes = new JokeDAO();
            Joke foundJoke = await Task.Run(() => jokes.FindId(id, _configuration));

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

            JokeDAO jokes = new JokeDAO();
            Joke foundJoke = await Task.Run(() => jokes.FindId(id, _configuration));
           
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
            JokeDAO jokes = new JokeDAO();
            jokes.Update(joke, _configuration);
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

            JokeDAO jokes = new JokeDAO();
            Joke joke = await Task.Run(() => jokes.FindId(id, _configuration));

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
            JokeDAO jokes = new JokeDAO();
            Joke joke = await Task.Run(() => jokes.FindId(id, _configuration));
            jokes.Delete(joke, _configuration);
            TempData["success"] = "Joke Deleted";

            return RedirectToAction("Index");
        }
    }
}
