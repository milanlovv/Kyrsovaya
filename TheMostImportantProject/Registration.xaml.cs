using System.Windows;
using System.Windows.Input;

namespace TheMostImportantProject
{
	public partial class Registration : Window
	{
		public Registration()
		{
			InitializeComponent();
		}

		private void ButtonRegister_Click(object sender, RoutedEventArgs e)
		{
			if (Password.Password == ConfirmPassword.Password)
			{
				using (Pizza_MozzarellaEntities db = new Pizza_MozzarellaEntities())
				{
					var entity = new Client()
					{
						LastName = LastName.Text,
						FirstName = FirstName.Text,
						DateOfBirth = DateOfBirth.SelectedDate,
						PhoneNumber = PhoneNumber.Text,
						Password = Password.Password
					};

					db.Client.Add(entity);

					try
					{
						db.SaveChanges();
						MessageBox.Show("Успешная регистрация!");

						var clientRepository = new ClientRepository(db);
						var authorizationService = new AuthorizationService(clientRepository);
						Authorization Authorization = new Authorization(authorizationService);
						Authorization.Show();
						this.Close();
					}
					catch
					{
						MessageBox.Show("Произошла ошибка при регистрации. Попробуйте еще раз.");
					}
				}
			}
			else
			{
				MessageBox.Show("Пароли не совпадают. Пожалуйста, введите пароль еще раз.");
			}
		}

		private void ButtonGoToLogin_Click(object sender, RoutedEventArgs e)
		{
			using (Pizza_MozzarellaEntities db = new Pizza_MozzarellaEntities())
			{
				var clientRepository = new ClientRepository(db);
				var authorizationService = new AuthorizationService(clientRepository);
				Authorization Authorization = new Authorization(authorizationService);
				Authorization.Show();
			}
		}

		private void PhoneNumber_KeyDown(object sender, KeyEventArgs e)
		{
			if (!(e.Key >= Key.D0 && e.Key <= Key.D9))
			{
				e.Handled = true;
			}
		}
	}
}
