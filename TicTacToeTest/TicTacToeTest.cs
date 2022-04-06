using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tic_Tac_Toe.Entities;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using System.Threading;
using Newtonsoft.Json;

namespace TicTacToeTest
{
    [TestClass]
    public class TicTacToeTest
    {
        [TestMethod]
        public async Task TestPost()
        {
            const string testContent = "test";
            
            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(testContent)
                });

            var underTest = new TicTacToe(new HttpClient(mockMessageHandler.Object));

            var response = await underTest.Post("http://test");

            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task TestCreateGameResponse()
        {
            const string testContent = "{ \"boardId\": \"testID\" }";

            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(testContent)
                });

            HttpClient testHttpClient = new HttpClient(mockMessageHandler.Object);
            testHttpClient.BaseAddress = new Uri("http://test");
            var underTest = new TicTacToe(testHttpClient);
            CreateGameResponse response = await underTest.CreateGameResponse(Difficulty.Easy);

            CreateGameResponse? createGameResponse = new CreateGameResponse();
            createGameResponse = JsonConvert.DeserializeObject<CreateGameResponse>(testContent);            
            
            Assert.AreEqual(response.BoardId, createGameResponse.BoardId);
        }
    }
}