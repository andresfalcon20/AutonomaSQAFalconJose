using System.Text.Json;
using AutonomaSQAFalconJose.Models;

namespace AutonomaSQAFalconJose.Service
{
    public class PokemonService
    {
        private readonly HttpClient _clienteHttp;

        public PokemonService(HttpClient clienteHttp)
        {
            _clienteHttp = clienteHttp;
        }

        public async Task<List<Pokemon>> ObtenerCatalogoPokemones()
        {
            var catalogo = new List<Pokemon>();
            var respuesta = await _clienteHttp.GetAsync("https://pokeapi.co/api/v2/pokemon?limit=20");

            if (respuesta.IsSuccessStatusCode)
            {
                var jsonOriginal = await respuesta.Content.ReadAsStringAsync();
                var datos = JsonSerializer.Deserialize<RespuestaApiPokemon>(jsonOriginal);

                if (datos?.Resultados != null)
                {
                    foreach (var item in datos.Resultados)
                    {
                        var respuestaDetalle = await _clienteHttp.GetAsync(item.Url);
                        if (respuestaDetalle.IsSuccessStatusCode)
                        {
                            var jsonDetalle = await respuestaDetalle.Content.ReadAsStringAsync();
                            var infoPokemon = JsonSerializer.Deserialize<DetallesPokemonRespuesta>(jsonDetalle);

                            if (infoPokemon != null)
                            {
                                catalogo.Add(new Pokemon
                                {
                                    Id = infoPokemon.Id,
                                    Nombre = infoPokemon.Name,
                                    FotoUrl = infoPokemon.Sprites.FrontDefault
                                });
                            }
                        }
                    }
                }
            }
            return catalogo;
        }

        public async Task<DetallesPokemonRespuesta> ObtenerPokemonPorId(int id)
        {
            var respuesta = await _clienteHttp.GetAsync($"https://pokeapi.co/api/v2/pokemon/{id}");

            if (respuesta.IsSuccessStatusCode)
            {
                var jsonDetalle = await respuesta.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<DetallesPokemonRespuesta>(jsonDetalle);
            }

            return null;
        }
    }
}