using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonGo.RocketAPI.GeneratedCode;

namespace PokemonGo.RocketAPI.Helpers
{
    public class Utils
    {
        public static ulong FloatAsUlong(double value)
        {
            var bytes = BitConverter.GetBytes(value);
            return BitConverter.ToUInt64(bytes, 0);
        }

        public static PokemonData PokemonToData(Pokemon pokemon)
        {
            return new PokemonData
            {
                Cp = pokemon.Cp,
                PokemonId = pokemon.PokemonType,
                Id = (ulong)pokemon.Id,

            };
        }

    }
}
