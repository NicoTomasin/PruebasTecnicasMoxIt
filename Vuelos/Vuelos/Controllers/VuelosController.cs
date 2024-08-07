using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vuelos.Models;
using Vuelos.Repository;

namespace Vuelos.Controllers
{

    public class VuelosController : Controller
    {
        private readonly ILogger<VuelosController> _logger;
        private readonly IVuelosRepository _vuelosRepository;

        public VuelosController(ILogger<VuelosController> logger, IVuelosRepository vuelosRepository)
        {
            _logger = logger;
            _vuelosRepository = vuelosRepository;
        }

        public async Task<IActionResult> Index()
        {
            var vuelos = await _vuelosRepository.GetAllAsync();
            return View(vuelos);
        }
        [HttpGet]
        public IActionResult Alta() { 
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Baja()
        {
            var vuelos = await _vuelosRepository.GetFlightNumberAsync();
            return View(vuelos);
        }
        [HttpGet]
        public async Task<IActionResult> Modificacion()
        {
            var vuelos = await _vuelosRepository.GetAllAsync();
            ViewBag.VuelosList = vuelos.ToList();
            return View(new VuelosModel());
        }
        [HttpPost]
        public async Task<IActionResult> Alta(VuelosModel model)
        {
            if (ModelState.IsValid)
            {
                var existingFlight = await _vuelosRepository.ExistAnyAsync(model.FlightNumber);
                if (existingFlight)
                {
                    ModelState.AddModelError("FlightNumber", "El número de vuelo ya existe.");
                    return View(model);
                }
                await _vuelosRepository.AddAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Baja(string flightNumber)
        {
            if (string.IsNullOrEmpty(flightNumber))
            {
                return RedirectToAction("Index");
            }
            await _vuelosRepository.DeleteFlightAsync(flightNumber);
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult> Modificacion(VuelosModel model)
        {
            if (ModelState.IsValid)
            {
                await _vuelosRepository.UpdateFlightAsync(model);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index"); // error
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
