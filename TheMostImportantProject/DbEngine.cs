using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMostImportantProject
{
	public interface IEntity
	{
		// Определите необходимые свойства или методы для сущности
		int Zayavka_Id { get; set; }
		string Name { get; set; }
		string Number { get; set; }
		string Time { get; set; }
	}

	public class Zayavka : IEntity
	{
		public int Zayavka_Id { get; set; }
		public string Name { get; set; }
		public string Number { get; set; }
		public string Time { get; set; }
	}

	public class DbEngine
	{
		private readonly string _connectionString = "server=127.0.0.1;port=3306;database=mydb1;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;";

		public void Fill<T>(IList<T> list, string command)
			where T : IEntity, new()
		{
			using (var con = new MySqlConnection(_connectionString))
			{
				con.Open();
				using (var cmd = new MySqlCommand(command, con))
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						var item = new T();
						item.Zayavka_Id = reader.GetInt32(0);
						item.Name = reader.GetString(1);
						item.Number = reader.GetString(2);
						item.Time = reader.GetString(3);
						list.Add(item);
					}
				}
			}
		}
	}
}
