using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PPPKZadatak03.Models;
using PPPKZadatak03.Service;

namespace PPPKZadatak03.Controllers
{
    public class ActorController : Controller
    {
        private readonly ILogger<ActorController>? _logger;
        private readonly ActorService? _actorService;

        public ActorController(ILogger<ActorController>? logger, ActorService? actorService)
        {
            _logger = logger;
            _actorService = actorService;
        }

        // GET: ActorController
        public ActionResult Index()
        {
            var allActors = _actorService?.GetAll();
            return View(allActors);
        }

        // GET: ActorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ActorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Actor actor)
        {
            try
            {
                var allActors = _actorService?.GetAll();
                var checkIfActorExists = allActors?.FirstOrDefault(a => a.Name == actor.Name);

                if (checkIfActorExists == null)
                {
                    _actorService?.Add(actor);

                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "The actor already exists!");
                return View(actor);

            }
            catch
            {
                return View();
            }
        }

        // GET: ActorController/Edit/5
        public ActionResult Edit(int id)
        {
            var actor = _actorService?.GetById(id);
            return View(actor);
        }

        // POST: ActorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Actor actor)
        {
            try
            {
                var actorDB = _actorService?.GetById(id);
                if (actorDB != null)
                {
                    actorDB.Name = actor.Name;

                    _actorService?.Update(actorDB);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ActorController/Delete/5
        public ActionResult Delete(int id)
        {
            var actor = _actorService?.GetById(id);
            return View(actor);
        }

        // POST: ActorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _actorService?.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
