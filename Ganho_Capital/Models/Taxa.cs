using Newtonsoft.Json;

namespace Ganho_Capital.Models
{
    public class Taxa
    {
        [JsonProperty("tax")]
        public decimal TaxaOperacao { get; set; }
    }
}