using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApplication1.Controllers;

namespace WebApplication1.Tests
{
	[TestClass]
	public class HomeControllerTests
	{
		private Mock<ILogger<HomeController>> _mockLogger;
		private HomeController _controller;

		[TestInitialize]
		public void TestInitialize()
		{
			_mockLogger = new Mock<ILogger<HomeController>>();
			_controller = new HomeController(_mockLogger.Object);
		}

		[TestMethod]
		public void Index_ReturnsViewResult()
		{
			var result = _controller.Index();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}

		[TestMethod]
		public void Privacy_ReturnsViewResult()
		{
			var result = _controller.Privacy();
			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}
		[TestMethod]
		public void Error_ReturnsViewResult()
		{
			// Arrange
			var mockHttpContext = new Mock<HttpContext>();
			mockHttpContext.Setup(c => c.TraceIdentifier).Returns("someValue");
			_controller.ControllerContext.HttpContext = mockHttpContext.Object;

			// Act
			var result = _controller.Error();

			// Assert
			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}

		// This is just a simple test, in a real world scenario you'd want to test different paths the method execution could take.
		[TestMethod]
		public void TaskThird_Post_ReturnsRedirectResult()
		{
			var result = _controller.TaskThird("John", "1234567890", "1");
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
		}

		
		
		[TestMethod]
		public void TaskThird_Post_InvalidInput_ReturnsContentResult()
		{
			// Arrange
			var homeController = new HomeController(null);

			// Act
			var result = homeController.TaskThird("Test Name", "1234567890", "invalid");

			// Assert
			Assert.IsInstanceOfType(result, typeof(ContentResult));
			var contentResult = result as ContentResult;
			Assert.AreEqual("<script language='javascript' type='text/javascript'>alert('Invalid input');</script>", contentResult.Content);
		}

	}
}
