using Newtonsoft.Json;
using System.Text;

namespace Tic_Tac_Toe.Entities
{
    public class TicTacToe
    {
        private HttpClient client;
        private bool lastCallSuccessful { get; set; }
        private ErrorMessage errorMessage;
        private const string createGameString = "tictactoe/game?difficulty={0}";

        public TicTacToe(HttpClient httpClient)
        {
            client = httpClient;
            errorMessage = new ErrorMessage();
        }

        public async Task<string> Post(string URL)
        {
            HttpResponseMessage httpResponse = await client.PostAsync(URL, null);

            var responseString = await httpResponse.Content.ReadAsStringAsync();

            return responseString;
        }

        public async Task<CreateGameResponse> CreateGameResponse(Difficulty difficulty)
        {
            string URL = String.Format(createGameString, difficulty);

            CreateGameResponse? createGameResponse = new CreateGameResponse();

            var response = await Post(URL);

            createGameResponse = JsonConvert.DeserializeObject<CreateGameResponse>(response);

            return createGameResponse;
        }

        public async Task<MakeNextMoveResponse> MakeNextMove(string boardId, int index)
        {
            string URL = $"tictactoe/game/{boardId}";

            MakeNextMoveRequest makeNextMoveRequest = new MakeNextMoveRequest()
            {
                Index = index
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(makeNextMoveRequest), Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await client.PostAsync(URL, content);

            lastCallSuccessful = httpResponse.IsSuccessStatusCode;

            var responseString = await httpResponse.Content.ReadAsStringAsync();

            if (lastCallSuccessful)
            {               
                MakeNextMoveResponse response = new MakeNextMoveResponse();

                response = JsonConvert.DeserializeObject<MakeNextMoveResponse>(responseString);

                return response;
            }
            else
            {
                errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(responseString);
                return null;
            }
        }

        public async Task<BoardResponse> BoardAsync(string boardId)
        {
            string URL = $"tictactoe/game/{boardId}";

            HttpResponseMessage httpResponse = await client.GetAsync(URL);

            lastCallSuccessful = httpResponse.IsSuccessStatusCode;

            var responseString = await httpResponse.Content.ReadAsStringAsync();

            if (lastCallSuccessful)
            {
                BoardResponse response = new BoardResponse();

                if (responseString != null)
                    response = JsonConvert.DeserializeObject<BoardResponse>(responseString);

                return response;
            }
            else
            {
                errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(responseString);
                return null;
            }
        }

        public async Task<string> GameBoardString(string boardId)
        {
            string URL = $"tictactoe/game/{boardId}/print";

            HttpResponseMessage httpResponse = await client.GetAsync(URL);

            var responseString = await httpResponse.Content.ReadAsStringAsync();

            return responseString;
        }
    }
}
