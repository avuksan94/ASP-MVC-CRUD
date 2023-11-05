using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PPPKZadatak03.Models;
using PPPKZadatak03.Service;

namespace PPPKZadatak03.Controllers
{
    public class DirectorController : Controller
    {
        private readonly ILogger<DirectorController>? _logger;
        private readonly DirectorService? _directorService;

        public DirectorController(ILogger<DirectorController>? logger, DirectorService? directorService)
        {
            _logger = logger;
            _directorService = directorService;
        }


        // GET: DirectorController
        public ActionResult Index()
        {
            var allDirectors = _directorService?.GetAll();
            return View(allDirectors);
        }

        // GET: DirectorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DirectorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DirectorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Director director)
        {
            try
            {
                var allDirectors = _directorService?.GetAll();
                var checkIfDirectorExists = allDirectors?.FirstOrDefault(a => a.Name == director.Name);

                if (checkIfDirectorExists == null)
                {
                    _directorService?.Add(director);

                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "The director already exists!");
                return View(director);

            }
            catch
            {
                return View();
            }
        }

        // GET: DirectorController/Edit/5
        public ActionResult Edit(int id)
        {
            var director = _directorService?.GetById(id);
            return View(director);
        }

        // POST: DirectorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Director director)
        {
            try
            {
                var directorDB = _directorService?.GetById(id);
                if (directorDB != null)
                {
                    directorDB.Name = director.Name;

                    _directorService?.Update(directorDB);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DirectorController/Delete/5
        public ActionResult Delete(int id)
        {
            var director = _directorService?.GetById(id);
            return View(director);
        }

        // POST: DirectorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _directorService?.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
