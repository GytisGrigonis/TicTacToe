using Newtonsoft.Json;

namespace Tic_Tac_Toe.Entities
{
    public class CreateGameResponse
    {
        [JsonProperty("boardId")]
        public string BoardId { get; set; }
    }
}
