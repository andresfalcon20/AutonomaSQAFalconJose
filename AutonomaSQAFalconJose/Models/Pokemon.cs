using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace AutonomaSQAFalconJose.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string FotoUrl { get; set; }
    }

    public class RespuestaApiPokemon
    {
        [JsonPropertyName("results")]
        public List<ResultadoPokemon> Resultados { get; set; }
    }

    public class ResultadoPokemon
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class DetallesPokemonRespuesta
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("weight")]
        public int Weight { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("sprites")]
        public SpritesPokemon Sprites { get; set; }
    }

    public class SpritesPokemon
    {
        [JsonPropertyName("front_default")]
        public string FrontDefault { get; set; }
    }
}