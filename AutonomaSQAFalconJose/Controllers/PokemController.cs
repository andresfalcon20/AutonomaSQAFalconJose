using Microsoft.AspNetCore.Mvc;
using AutonomaSQAFalconJose.Service;

namespace AutonomaSQAFalconJose.Controllers
{
    public class PokemonController : Controller
    {
        private readonly PokemonService _servicioPokemon;

        public PokemonController(PokemonService servicioPokemon)
        {
            _servicioPokemon = servicioPokemon;
        }

        public async Task<IActionResult> Index()
        {
            var miListaPokemones = await _servicioPokemon.ObtenerCatalogoPokemones();
            return View(miListaPokemones);
        }

        public async Task<IActionResult> Details(int id)
        {
            var pokemon = await _servicioPokemon.ObtenerPokemonPorId(id);

            if (pokemon == null)
            {
                return RedirectToAction("Index"); 
            }

            return View(pokemon);
        }
    }
}