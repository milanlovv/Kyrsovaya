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
using Windows.UI.Xaml.Media.Imaging;
using MySqlConnector;

namespace App8
{
	public sealed partial class Uslugi : Page
	{
		public class Service
		{
			public BitmapImage Image { get; set; }
			public string Name { get; set; }
			public decimal Price { get; set; }
			public int Index { get; set; }
		}

		public Uslugi()
		{
			this.InitializeComponent();
		}


		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			string connectionString = "server=127.0.0.1;port=3306;database=pizza_mozzarella;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;";

			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				await connection.OpenAsync();
				MySqlCommand command = new MySqlCommand("SELECT * FROM position WHERE ishidden = 0", connection);

				int index = 1;
				using (var reader = await command.ExecuteReaderAsync())
				{
					while (await reader.ReadAsync())
					{
						byte[] imageBytes = (byte[])reader["image"];
						using (var ms = new MemoryStream(imageBytes))
						{
							var image = new BitmapImage();
							await image.SetSourceAsync(ms.AsRandomAccessStream());
							var service = new Service
							{
								Image = image,
								Name = reader.GetString(reader.GetOrdinal("name")),
								Price = reader.GetDecimal(reader.GetOrdinal("price")),
								Index = index++
							};

							UslugiListView.Items.Add(service);
						}
					}
				}
			}
		}

		private void BuyButton_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new Windows.UI.Popups.MessageDialog("Чтобы купить, позвоните на номер 89656021541", "Купить");
			dialog.ShowAsync();
		}

		private void ZayavkiButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(Client));
		}
	}
}
