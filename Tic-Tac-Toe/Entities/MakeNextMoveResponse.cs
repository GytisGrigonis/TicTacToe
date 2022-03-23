using Newtonsoft.Json;

namespace Tic_Tac_Toe.Entities
{
    public class MakeNextMoveResponse
    {
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
