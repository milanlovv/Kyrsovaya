using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace App8
{
	public sealed partial class Registration : Page
	{
		private readonly HttpClient _client = new HttpClient();

		public Registration()
		{
			this.InitializeComponent();
		}

		private async void RegisterButton_Click(object sender, RoutedEventArgs e)
		{
			if (PasswordBox.Password != ConfirmPasswordBox.Password)
			{
				MessageDialog dialog = new MessageDialog("Пароли не совпадают. Пожалуйста, попробуйте еще раз.");
				await dialog.ShowAsync();
				return;
			}

			DateTime dateOfBirth = DateOfBirthPicker.Date.DateTime;

			var clientObject = new App8.Models.Client
			{
				LastName = LastNameTextBox.Text,
				FirstName = FirstNameTextBox.Text,
				Password = PasswordBox.Password,
				DateOfBirth = dateOfBirth,
				PhoneNumber = PhoneNumberTextBox.Text
			};

			var jsonString = JsonSerializer.Serialize(clientObject);
			var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

			var response = await _client.PostAsync("https://localhost:7141/Client/CreateClientFromApp", httpContent);

			if (response.IsSuccessStatusCode)
			{
				var clientId = JsonSerializer.Deserialize<int>(await response.Content.ReadAsStringAsync());
				Frame.Navigate(typeof(MainPage), clientId);
			}
			else
			{
				MessageDialog dialog = new MessageDialog("Ошибка при регистрации. Пожалуйста, попробуйте еще раз.");
				await dialog.ShowAsync();
			}
		}

		private void LoginButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainPage));
		}
	}
}
