using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace TheMostImportantProject
{
	public interface IClientRepository
	{
		Client GetClientByPhoneNumber(string phoneNumber);
	}

	public class ClientRepository : IClientRepository
	{
		private readonly Pizza_MozzarellaEntities _dbContext;

		public ClientRepository(Pizza_MozzarellaEntities dbContext)
		{
			_dbContext = dbContext;
		}

		public Client GetClientByPhoneNumber(string phoneNumber)
		{
			return _dbContext.Client.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
		}
	}

	public interface IAuthorization
	{
		AuthorizationResult Authorize(string phoneNumber, string password);
	}

	public enum AuthorizationResult
	{
		Success,
		InvalidPassword,
		PhoneNumberNotRegistered
	}

	public class AuthorizationService : IAuthorization
	{
		private readonly IClientRepository _clientRepository;

		public AuthorizationService(IClientRepository clientRepository)
		{
			_clientRepository = clientRepository;
		}

		public AuthorizationResult Authorize(string phoneNumber, string password)
		{
			using (var dbContext = new Pizza_MozzarellaEntities())
			{
				var repository = new ClientRepository(dbContext);

				if (string.IsNullOrEmpty(phoneNumber))
				{
					throw new ArgumentException("Phone number can't be null or empty.", nameof(phoneNumber));
				}

				var client = repository.GetClientByPhoneNumber(phoneNumber);
				if (client == null)
				{
					return AuthorizationResult.PhoneNumberNotRegistered;
				}

				if (client.Password != password)
				{
					return AuthorizationResult.InvalidPassword;
				}

				CurrentUser.Id = client.ClientID;
				return AuthorizationResult.Success;
			}
		}
	}


	public partial class Authorization : Window
	{
		private readonly IAuthorization _authorizationService;

		public Authorization(IAuthorization authorizationService)
		{
			_authorizationService = authorizationService;
			InitializeComponent();
		}

		public Authorization()
		{
		}

		private void ButtonEnter_Click(object sender, RoutedEventArgs e)
		{
			AuthorizationResult result = _authorizationService.Authorize(PhoneNumber.Text, Password.Password);

			switch (result)
			{
				case AuthorizationResult.Success:
					MenuScreen menuScreen = new MenuScreen();
					menuScreen.Show();
					this.Close();
					break;
				case AuthorizationResult.InvalidPassword:
					MessageBox.Show("Введён неверный пароль");
					break;
				case AuthorizationResult.PhoneNumberNotRegistered:
					MessageBox.Show("Такой номер телефона не зарегистрирован");
					break;
			}
		}

		private void PhoneNumber_KeyDown(object sender, KeyEventArgs e)
		{
			if (!(e.Key >= Key.D0 && e.Key <= Key.D9))
			{
				e.Handled = true;
			}
		}

		private void ButtonEnter_Click2(object sender, RoutedEventArgs e)
		{
			AdminAuthorization menuScreen = new AdminAuthorization(); // Здесь вы передаете 0 аргументов
			menuScreen.Show();
			this.Close();
		}

	}
}
