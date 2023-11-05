using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PPPKZadatak03.Models;
using PPPKZadatak03.Service;

namespace PPPKZadatak03.Controllers
{
    public class PosterController : Controller
    {
        private readonly ILogger<PosterController>? _logger;
        private readonly PosterService? _posterService;

        public PosterController(ILogger<PosterController>? logger, PosterService? posterService)
        {
            _logger = logger;
            _posterService = posterService;
        }


        // GET: PosterController
        public ActionResult Index()
        {
            var allPosters = _posterService?.GetAll();
            return View(allPosters);
        }

        // GET: PosterController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PosterController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PosterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Poster poster)
        {
            try
            {
                var allPosters = _posterService?.GetAll();
                var checkIfPosterExists = allPosters?.FirstOrDefault(p => p.Content == poster.Content);

                if (checkIfPosterExists == null)
                {
                    _posterService?.Add(poster);

                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "The poster already exists!");
                return View(poster);

            }
            catch
            {
                return View();
            }
        }

        // GET: PosterController/Edit/5
        public ActionResult Edit(int id)
        {
            var poster = _posterService?.GetById(id);
            return View(poster);
        }

        // POST: PosterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Poster poster)
        {
            try
            {
                var posterDB = _posterService?.GetById(id);
                if (posterDB != null)
                {
                    posterDB.Content = poster.Content;

                    _posterService?.Update(posterDB);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PosterController/Delete/5
        public ActionResult Delete(int id)
        {
            var poster = _posterService?.GetById(id);
            return View(poster);
        }

        // POST: PosterController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _posterService?.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
