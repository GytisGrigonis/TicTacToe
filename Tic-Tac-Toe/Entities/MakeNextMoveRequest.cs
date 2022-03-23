using Newtonsoft.Json;

namespace Tic_Tac_Toe.Entities
{
    public class MakeNextMoveRequest
    {              
        [JsonProperty("index")]
        public int Index { get; set; }
    }
}
