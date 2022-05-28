using Newtonsoft.Json;

namespace Ganho_Capital.Models
{
    public class Acao
    {
        [JsonProperty("operation")]
        public string Operation { get; set; }

        [JsonProperty("unit-cost")]
        public decimal UnitCost { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        public decimal Imposto { get; set; }
    }
}