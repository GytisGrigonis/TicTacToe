
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
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://194.31.55.57/");

            TicTacToe API = new TicTacToe(httpClient);

            CreateGameResponse game = await API.CreateGameResponse(Difficulty.Easy);

            BoardResponse board = await API.BoardAsync(game.BoardId);

            string boardString = await API.GameBoardString(game.BoardId);

            MakeNextMoveResponse nextMove;

            Printer printer = new Printer();

            if (board.FirstMove == "You (X)")
            {
                printer.printPlayerStarts();
            }
            else
            {
                printer.printMachineStarts();
            }

            printer.printCustom(boardString);

            while (doWhile)
            {
                printer.printEnterMove();
                input = Convert.ToInt16(Console.ReadLine());
                //checkas inputo
                nextMove = await API.MakeNextMove(game.BoardId, input);
                boardString = await API.GameBoardString(game.BoardId);
                printer.printCustom(boardString);
                if (nextMove.State == "finished")
                {
                    printer.printCustom(nextMove.Result);
                    doWhile = false;
                }
            }
        }
    }
}