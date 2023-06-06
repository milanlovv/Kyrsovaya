using System;
using System.Collections.Generic;
using System.Data;

using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
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
using MySqlConnector;

namespace App8
{
	public sealed partial class Client : Page
	{
		public Client()
		{
			this.InitializeComponent();
		}

		public string LastName { get; internal set; }
		public string FirstName { get; internal set; }
		public string Password { get; internal set; }
		public string DateOfBirth { get; internal set; }
		public string PhoneNumber { get; internal set; }

		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			string phoneNumber = e.Parameter as string;

			// Set the phone number in the text block
			ClientNumberTextBlock.Text = $"Ваш номер: {Class1.phoneNumber}";

			string connectionString = "server=127.0.0.1;port=3306;database=pizza_mozzarella;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;";

			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				await connection.OpenAsync();
				MySqlCommand command = new MySqlCommand("SELECT * FROM zayavka WHERE number = @phoneNumber", connection);

				command.Parameters.AddWithValue("@phoneNumber", Class1.phoneNumber);

				int index = 1;
				using (var reader = await command.ExecuteReaderAsync())
				{
					while (await reader.ReadAsync())
					{
						var order = new
						{
							Index = index++,
							Name = reader.GetString(reader.GetOrdinal("name")),
							Number = reader.GetString(reader.GetOrdinal("number")),
							Time = reader.GetString(reader.GetOrdinal("time"))
						};
						OrdersListView.Items.Add(order);
					}
				}
			}
		}

		private void UslugiButton_Click(object sender, RoutedEventArgs e)
		{
			// Navigate to the Uslugi page
			Frame.Navigate(typeof(Uslugi));
		}
	}
}