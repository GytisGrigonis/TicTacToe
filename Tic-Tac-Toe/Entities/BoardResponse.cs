using ConsoleProgram;
using Newtonsoft.Json;

namespace Tic_Tac_Toe.Entities
{
    public class BoardResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("difficulty")]
        public Difficulty Difficulty { get; set; }
        [JsonProperty("firstMove")]
        public string FirstMove { get; set; }
        [JsonProperty("layout")]
        public string[] Layout
        { get; set; }
    }
}
