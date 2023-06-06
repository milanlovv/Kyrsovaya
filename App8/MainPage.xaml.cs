using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Reflection;

namespace App8
{
	public static class ServiceLocator
	{
		private static HttpClientService _httpClientService;
		public static IHttpClientService HttpClientService
		{
			get
			{
				if (_httpClientService == null)
				{
					_httpClientService = new HttpClientService(new HttpClient());
				}

				return _httpClientService;
			}
		}
	}

	public sealed partial class MainPage : Page
	{
		private readonly IHttpClientService _clientService;

		public MainPage()
		{
			this.InitializeComponent();
			_clientService = ServiceLocator.HttpClientService;
		}

		private async void SignIn_Click(object sender, RoutedEventArgs e)
		{
			string phoneNumber = PhoneNumberTextBox.Text;
			string password = PasswordBox.Password;

			try
			{
				var client = await _clientService.GetClientAsync(phoneNumber, password);

				if (client != null)
				{
					Class1.phoneNumber = phoneNumber;
					this.Frame.Navigate(typeof(Client), phoneNumber);
				}
				else
				{
					MessageDialog dialog = new MessageDialog("Неверный номер телефона или пароль.", "Ошибка авторизации");
					await dialog.ShowAsync();
				}
			}
			catch (Exception)
			{
				MessageDialog dialog = new MessageDialog("Произошла ошибка при подключении к серверу.", "Ошибка подключения");
				await dialog.ShowAsync();
			}
		}

		private void GoToRegistration_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(Registration));
		}
	}

	public interface IHttpClientService
	{
		Task<App8.Models.Client> GetClientAsync(string phoneNumber, string password);
	}

	public class HttpClientService : IHttpClientService
	{
		private HttpClient _client;

		public HttpClientService(HttpClient client)
		{
			_client = client;
		}

		public async Task<App8.Models.Client> GetClientAsync(string phoneNumber, string password)
		{
			var response = await _client.GetAsync($"https://localhost:7141/Client/FindClient?phoneNumber={phoneNumber}&password={password}");

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var client = JsonSerializer.Deserialize<App8.Models.Client>(content);
				return client;
			}

			return null;
		}
	}
}
