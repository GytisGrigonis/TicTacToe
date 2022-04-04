using Newtonsoft.Json;

namespace Tic_Tac_Toe.Entities
{
    public class ErrorMessage
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
