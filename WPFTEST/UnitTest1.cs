using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using TheMostImportantProject;

namespace WPFTEST
{
	[TestClass]
	public class AuthorizationServiceTests
	{
		private Mock<IClientRepository> _mockRepository;
		private AuthorizationService _authorizationService;

		[TestInitialize]
		public void TestInitialize()
		{
			_mockRepository = new Mock<IClientRepository>();
			_authorizationService = new AuthorizationService(_mockRepository.Object);
		}

		[TestMethod]
		public void TestAuthorize_ValidCredentials_Success()
		{
			_mockRepository.Setup(r => r.GetClientByPhoneNumber(It.IsAny<string>()))
							.Returns(new Client { PhoneNumber = "1234567890", Password = "password" });

			var result = _authorizationService.Authorize("1234567890", "password");

			Assert.AreEqual(AuthorizationResult.Success, result);
		}

		[TestMethod]
		public void TestAuthorize_InvalidPassword_InvalidPassword()
		{
			_mockRepository.Setup(r => r.GetClientByPhoneNumber(It.IsAny<string>()))
							.Returns(new Client { PhoneNumber = "1234567890", Password = "password" });

			var result = _authorizationService.Authorize("1234567890", "wrongpassword");

			Assert.AreEqual(AuthorizationResult.InvalidPassword, result);
		}

		[TestMethod]
		public void TestAuthorize_NotRegisteredNumber_PhoneNumberNotRegistered()
		{
			_mockRepository.Setup(r => r.GetClientByPhoneNumber(It.IsAny<string>()))
							.Returns((Client)null);

			var result = _authorizationService.Authorize("notregisterednumber", "password");

			Assert.AreEqual(AuthorizationResult.PhoneNumberNotRegistered, result);
		}

		[TestMethod]
		public void TestAuthorize_NullPhoneNumber_ArgumentException()
		{
			Assert.ThrowsException<ArgumentException>(() => _authorizationService.Authorize(null, "password"));
		}

		[TestMethod]
		public void TestAuthorize_EmptyPhoneNumber_ArgumentException()
		{
			Assert.ThrowsException<ArgumentException>(() => _authorizationService.Authorize("", "password"));
		}
	}
}
