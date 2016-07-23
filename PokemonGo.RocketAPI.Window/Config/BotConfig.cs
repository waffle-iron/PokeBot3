using System.Collections.Generic;
using System.IO;
using AllEnum;
using Newtonsoft.Json;
using PokemonGo.RocketAPI.Enums;

namespace PokemonGo.RocketAPI.Window.Config
{
    public sealed class BotConfig
    {
        private static volatile BotConfig _instance;
        private static readonly object SyncRoot = new object();

        public static BotConfig Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                lock (SyncRoot)
                {
                    if (_instance == null)
                        _instance = new BotConfig();
                }

                return _instance;
            }
        }

        public BotConfig ServerConfig;

        public void Initialize()
        {
            ServerConfig = JsonConvert.DeserializeObject<BotConfig>(File.ReadAllText(@"settings.json"));
        }
    }

    public class BotSettings
    {
        [JsonProperty(PropertyName = "authType")]
        public AuthType AuthType { get; set; }

        [JsonProperty(PropertyName = "defaultLat")]
        public double DefaultLatitude { get; set; }

        [JsonProperty(PropertyName = "defaultLong")]
        public double DefaultLongitude { get; set; }

        [JsonProperty(PropertyName = "levelOutput")]
        public string LevelOutput { get; set; }

        [JsonProperty(PropertyName = "levelTimeInterval")]
        public int LevelTimeInterval { get; set; }

        //[JsonProperty(PropertyName = "googlerefreshtoken")] Not yet
        public string GoogleRefreshToken { get; set; }

        [JsonProperty(PropertyName = "ptcPpassword")]
        public string PtcPassword { get; set; }

        [JsonProperty(PropertyName = "ptcUsername")]
        public string PtcUsername { get; set; }

        [JsonProperty(PropertyName = "evolveAllGivenPokemon")]
        public bool EvolveAllGivenPokemons { get; set; }

        [JsonProperty(PropertyName = "transferType")]
        public string TransferType { get; set; }

        [JsonProperty(PropertyName = "transferCPThreshold")]
        public int TransferCPThreshold { get; }

        //[JsonProperty(PropertyName = "itemRecycleFilter")] Not yet
        public ICollection<KeyValuePair<AllEnum.ItemId, int>> ItemRecycleFilter
        {
            get
            {
                //Type and amount to keep
                return new[]
                {
                    new KeyValuePair<ItemId, int>(ItemId.ItemPokeBall, 20),
                    new KeyValuePair<ItemId, int>(ItemId.ItemGreatBall, 50),
                    new KeyValuePair<ItemId, int>(ItemId.ItemUltraBall, 100),
                    new KeyValuePair<ItemId, int>(ItemId.ItemMasterBall, 200),
                    new KeyValuePair<ItemId, int>(ItemId.ItemRazzBerry, 20),
                    new KeyValuePair<ItemId, int>(ItemId.ItemRevive, 20),
                    new KeyValuePair<ItemId, int>(ItemId.ItemPotion, 0),
                    new KeyValuePair<ItemId, int>(ItemId.ItemSuperPotion, 0),
                    new KeyValuePair<ItemId, int>(ItemId.ItemHyperPotion, 50)
                };
            }
        }

        [JsonProperty(PropertyName = "itemRecycleInterval")]
        public int RecycleItemsInterval { get; set; }

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }
    }
}