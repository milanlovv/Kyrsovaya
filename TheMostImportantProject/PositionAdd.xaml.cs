using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using MySqlConnector;

namespace TheMostImportantProject
{
	public partial class PositionAdd : Window
	{
		Position currentPos = new Position();
		System.Drawing.Image positionImageToConvert;

		public PositionAdd(Position pos = null)
		{
			currentPos = pos;
			InitializeComponent();
			if (pos != null)
			{
				MemoryStream ms = new MemoryStream(pos.Image);
				PositionImage.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
				Name.Text = pos.Name;
				Price.Text = string.Format("{0:0.00}", pos.Price);
				IsHidden.IsChecked = pos.IsHidden;
				SaveBtn.Content = "Изменить позицию";
			}
		}

		private void SelectImage(object sender, RoutedEventArgs e)
		{
			OpenFileDialog pictureDialog = new OpenFileDialog();
			pictureDialog.Title = "Выбрать изображение";
			pictureDialog.Filter = "Image Files(*.JPG;*.PNG;*.GIF)|*.JPG;*PNG;*.GIF";

			if (pictureDialog.ShowDialog() == true)
			{
				BitmapImage positionPicture = new BitmapImage();
				positionPicture.BeginInit();
				positionPicture.UriSource = new Uri(pictureDialog.FileName);
				positionPicture.EndInit();

				positionImageToConvert = System.Drawing.Image.FromFile(pictureDialog.FileName);
				PositionImage.Source = positionPicture;
			}
		}

		private void Price_KeyDown(object sender, KeyEventArgs e)
		{
			if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.OemComma)
			{
				e.Handled = true;
			}
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			if (Name.Text == "" || Price.Text == "" || PositionImage.Source == null)
			{
				MessageBox.Show("Заполните все поля");
			}
			else
			{
				string connectionString = "server=127.0.0.1;port=3306;database=pizza_mozzarella;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;";

				using (MySqlConnection connection = new MySqlConnection(connectionString))
				{
					connection.Open();

					ImageConverter converter = new ImageConverter();
					var ImageConvert = converter.ConvertTo(positionImageToConvert, typeof(byte[]));
					byte[] image = (byte[])ImageConvert;

					if (currentPos == null)
					{
						string insertQuery = "INSERT INTO position (Name, Price, Image, IsHidden) VALUES (@Name, @Price, @Image, @IsHidden)";

						using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
						{
							command.Parameters.AddWithValue("@Name", Name.Text);
							command.Parameters.AddWithValue("@Price", Convert.ToDecimal(Price.Text));
							command.Parameters.AddWithValue("@Image", image);
							command.Parameters.AddWithValue("@IsHidden", (bool)IsHidden.IsChecked);

							command.ExecuteNonQuery();
						}

						MessageBox.Show("Позиция успешно добавлена");
					}
					else
					{
						string updateQuery = "UPDATE position SET Name = @Name, Price = @Price, Image = @Image, IsHidden = @IsHidden WHERE PositionID = @PositionID";

						using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
						{
							command.Parameters.AddWithValue("@Name", Name.Text);
							command.Parameters.AddWithValue("@Price", Convert.ToDecimal(Price.Text));
							command.Parameters.AddWithValue("@Image", image);
							command.Parameters.AddWithValue("@IsHidden", (bool)IsHidden.IsChecked);
							command.Parameters.AddWithValue("@PositionID", currentPos.PositionID);

							command.ExecuteNonQuery();
						}

						MessageBox.Show("Позиция успешно изменена");
					}
				}
			}
		}

		private void ToEmployees_MouseUp(object sender, MouseButtonEventArgs e)
		{
			Employees employees = new Employees();
			employees.Show();
			this.Close();
		}

		private void ToMenu_MouseUp(object sender, MouseButtonEventArgs e)
		{
			AdminMenu adminMenu = new AdminMenu();
			adminMenu.Show();
			this.Close();
		}
	}
}
