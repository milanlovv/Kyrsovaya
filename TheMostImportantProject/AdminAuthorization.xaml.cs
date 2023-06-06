using System.Net;
using System.Windows;

namespace TheMostImportantProject
{
	public partial class AdminAuthorization : Window
	{
		public AdminAuthorization()
		{
			InitializeComponent();
		}

		private void ButtonEnter_Click(object sender, RoutedEventArgs e)
		{
			bool adminFound = false;
			using (Pizza_MozzarellaEntities db = new Pizza_MozzarellaEntities())
			{
				foreach (var entity1 in db.Employee)
				{
					if (Login.Text == entity1.Login)
					{
						adminFound = true;
						if (Password.Password == entity1.Password)
						{
							Emplo menuScreen = new Emplo();
							menuScreen.Show();
							this.Close();
						}
					}
				}

				foreach (var entity in db.Administrator)
				{
					if (Login.Text == entity.Login)
					{
						adminFound = true;
						if (Password.Password == entity.Password)
						{
							AdminMenu menuScreen = new AdminMenu();
							menuScreen.Show();
							this.Close();
						}
						else
						{
							MessageBox.Show("Введён неверный пароль");
						}
					}
					if (!adminFound)
					{
						MessageBox.Show("Такого логина не существует");
					}
				}
			}
		}

		private void ButtonEnter_Click2(object sender, RoutedEventArgs e)
		{
			using (Pizza_MozzarellaEntities db = new Pizza_MozzarellaEntities())
			{
				var clientRepository = new ClientRepository(db);
				var authorizationService = new AuthorizationService(clientRepository);
				Authorization Authorization = new Authorization(authorizationService);
				Authorization.Show();
				this.Close();
			}
		}
	}
}
