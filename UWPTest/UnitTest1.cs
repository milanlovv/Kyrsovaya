using Moq;
using System.Net;
using System.Text;
using App8;
using Moq.Protected;
using System.Text.Json;

namespace UWPTest
{
	[TestClass]
	public class UnitTest1
	{
		private HttpClientService _clientService;
		private Mock<HttpMessageHandler> _mockHttpMessageHandler;

		[TestInitialize]
		public void TestInitialize()
		{
			_mockHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
			_clientService = new HttpClientService(new HttpClient(_mockHttpMessageHandler.Object));
		}

		[TestMethod]
		public async Task GetClientAsync_CorrectCredentials_ReturnsClient()
		{
			// Arrange
			string phoneNumber = "1234567890";
			string password = "password";
			var expectedClient = new App8.Models.Client { /* populate your client model */ };

			var httpResponse = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(JsonSerializer.Serialize(expectedClient), Encoding.UTF8, "application/json"),
			};

			_mockHttpMessageHandler
				.Protected()
				.Setup<Task<HttpResponseMessage>>(
					"SendAsync",
					ItExpr.IsAny<HttpRequestMessage>(),
					ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(httpResponse);

			// Act
			var client = await _clientService.GetClientAsync(phoneNumber, password);

			// Assert
			Assert.AreEqual(expectedClient, client);
		}


		[TestMethod]
		public async Task GetClientAsync_WrongCredentials_ReturnsNull()
		{
			// Arrange
			string phoneNumber = "wrongNumber";
			string password = "wrongPassword";

			var httpResponse = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.BadRequest,
			};

			_mockHttpMessageHandler
				.Protected()
				.Setup<Task<HttpResponseMessage>>(
					"SendAsync",
					ItExpr.IsAny<HttpRequestMessage>(),
					ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(httpResponse);

			// Act
			var client = await _clientService.GetClientAsync(phoneNumber, password);

			// Assert
			Assert.IsNull(client);
		}

		[TestMethod]
		public async Task GetClientAsync_InvalidPhoneNumber_ThrowsException()
		{
			// Arrange
			string phoneNumber = "not a phone number";
			string password = "password";

			_mockHttpMessageHandler
				.Protected()
				.Setup<Task<HttpResponseMessage>>(
					"SendAsync",
					ItExpr.IsAny<HttpRequestMessage>(),
					ItExpr.IsAny<CancellationToken>())
				.Throws<HttpRequestException>();

			// Act & Assert
			await Assert.ThrowsExceptionAsync<HttpRequestException>(
				() => _clientService.GetClientAsync(phoneNumber, password));
		}

		[TestMethod]
		public async Task GetClientAsync_EmptyPassword_ReturnsNull()
		{
			// Arrange
			string phoneNumber = "1234567890";
			string password = "";

			var httpResponse = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.BadRequest,
			};

			_mockHttpMessageHandler
				.Protected()
				.Setup<Task<HttpResponseMessage>>(
					"SendAsync",
					ItExpr.IsAny<HttpRequestMessage>(),
					ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(httpResponse);

			// Act
			var client = await _clientService.GetClientAsync(phoneNumber, password);

			// Assert
			Assert.IsNull(client);
		}

		[TestMethod]
		public async Task GetClientAsync_ServerUnavailable_ThrowsException()
		{
			// Arrange
			string phoneNumber = "1234567890";
			string password = "password";

			_mockHttpMessageHandler
				.Protected()
				.Setup<Task<HttpResponseMessage>>(
					"SendAsync",
					ItExpr.IsAny<HttpRequestMessage>(),
					ItExpr.IsAny<CancellationToken>())
				.Throws<TaskCanceledException>();

			// Act & Assert
			await Assert.ThrowsExceptionAsync<TaskCanceledException>(
				() => _clientService.GetClientAsync(phoneNumber, password));
		}
	}
}
