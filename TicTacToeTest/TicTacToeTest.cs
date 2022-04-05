using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tic_Tac_Toe.Entities;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TicTacToeTest
{
    [TestClass]
    public class TicTacToeTest
    {
        [TestMethod]
        public async Task TestCreateGameAsync()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://194.31.55.57/");

            TicTacToe testAPI = new TicTacToe(client);

            CreateGameResponse game = await testAPI.CreateGameAsync(Difficulty.Easy);

            Assert.IsNotNull(game);
        }
    }
}