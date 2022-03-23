
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Tic_Tac_Toe.Entities;

namespace ConsoleProgram
{
    public class Program
    {

        static public async Task Main(String[] args)
        {
            int input;
            bool doWhile = true;

            TicTacToe API = new TicTacToe();

            CreateGameResponse game = await API.CreateGameAsync(Difficulty.Easy);

            BoardResponse board = await API.BoardAsync(game.BoardId);

            string boardString = await API.GameBoardString(game.BoardId);

            MakeNextMoveResponse nextMove;

            if (board.FirstMove == "You (X)")
            {
                Console.WriteLine("you start");
            }
            else
            {
                Console.WriteLine("machine starts");
            }

            Console.WriteLine(boardString);

            while (doWhile)
            {
                Console.WriteLine("enter your move:");
                input = Convert.ToInt16(Console.ReadLine());
                //checkas inputo
                nextMove = await API.MakeNextMove(game.BoardId, input);
                boardString = await API.GameBoardString(game.BoardId);
                Console.WriteLine(boardString);
                if (nextMove.State == "finished")
                {
                    Console.WriteLine(nextMove.Result);
                    doWhile = false;
                }

            }
        }
    }

    public class TicTacToe
    {
        private HttpClient client;

        public TicTacToe()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://194.31.55.57/");
        }

        public async Task<CreateGameResponse> CreateGameAsync(Difficulty difficulty)
        {
            string URL = $"tictactoe/game?difficulty={difficulty}";

            HttpResponseMessage httpResponse = await client.PostAsync(URL, null);

            var responseString = await httpResponse.Content.ReadAsStringAsync();

            CreateGameResponse response = new CreateGameResponse();

            if (responseString != null)
                response = JsonConvert.DeserializeObject<CreateGameResponse>(responseString);

            return response;
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

            var responseString = await httpResponse.Content.ReadAsStringAsync();

            MakeNextMoveResponse response = new MakeNextMoveResponse();

            if (responseString != null)
                response = JsonConvert.DeserializeObject<MakeNextMoveResponse>(responseString);

            return response;
        }

        public async Task<BoardResponse> BoardAsync(string boardId)
        {
            string URL = $"tictactoe/game/{boardId}";

            HttpResponseMessage httpResponse = await client.GetAsync(URL);

            var responseString = await httpResponse.Content.ReadAsStringAsync();

            BoardResponse response = new BoardResponse();

            if (responseString != null)
                response = JsonConvert.DeserializeObject<BoardResponse>(responseString);

            return response;
        }

        public async Task<string> GameBoardString(string boardId)
        {
            string URL = $"tictactoe/game/{boardId}/print";

            HttpResponseMessage httpResponse = await client.GetAsync(URL);

            var responseString = await httpResponse.Content.ReadAsStringAsync();

            return responseString;
        }
    }

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
}