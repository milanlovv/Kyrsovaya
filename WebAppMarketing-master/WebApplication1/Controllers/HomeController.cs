using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Repository;
using WebApplication1.Repository.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {


		private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
		
		}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult TaskFirst()
        {
            return View();
        }

        public IActionResult TaskSecond()
        {
            return View();
        }

		public IActionResult TaskThird()

		{

			string connectionString = @"Data Source=LAPTOP-DTQF99UB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Encrypt=false;";
			string sqlExpression = "insert into Client (FirstName, LastName, Password, PhoneNumber, DateOfBirth) values('','' , '','', '1900-01-01 00:00:00.000')";





			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				// добавление
				SqlCommand command = new SqlCommand(sqlExpression, connection);



			}
			return View();
		}

		public IActionResult Create()
		{
			return View();
		}


		//      [HttpPost]
		//      [ValidateAntiForgeryToken]
		//      public IActionResult TaskThird([Bind("ClientId,FirstName,Password,PhoneNumber")] Client client)
		//      {
		//	//         using (Pizza_MozzarellaDbContext db = new Pizza_MozzarellaDbContext())
		//	//         {
		//	//                 db.Add(client);
		//	//                 db.SaveChangesAsync();

		//	//		return RedirectToAction(nameof(Privacy));

		//	//         }

		//	//return RedirectToAction(nameof(Index));



		//	MySqlConnection conn = new MySqlConnection("Data Source=LAPTOP-DTQF99UB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Encrypt=false;");
		//	try
		//	{
		//		if (conn.State == ConnectionState.Closed)
		//		{
		//			conn.Open();
		//			MySqlCommand cmd = new MySqlCommand("INSERT Client VALUES ('', '', '', '', '')", conn);


		//		}
		//	}
		//	catch { Content("<script language='javascript' type='text/javascript'>alert(ex.ToString());</script>"); }
		//	return RedirectToAction(nameof(Index));
		//}
		[HttpPost]
		public IActionResult TaskThird(string contactName, string contactNumber, string Password)
		{

			string connectionString = @"Data Source=LAPTOP-DTQF99UB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Encrypt=false;";
			string sqlExpression = "insert into Client (FirstName, LastName, Password, PhoneNumber, DateOfBirth) values('','' , '','', '1900-01-01 00:00:00.000')";





			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				// добавление
				SqlCommand command = new SqlCommand(sqlExpression, connection);
				
				

			}


			//MySqlConnection conn = new MySqlConnection("Data Source=LAPTOP-DTQF99UB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Encrypt=false;");
			//try
			//{
			//	if (conn.State == ConnectionState.Closed)
			//	{
			//		conn.Open();
			//		MySqlCommand cmd1 = new MySqlCommand("select max(manager_id) from manager", conn);
			//		int log = Convert.ToInt32(cmd1.ExecuteScalar());
			//		Random r = new Random();
			//		int idd = r.Next(1, log + 1);
			//		MySqlCommand cmd = new MySqlCommand("insert into Client (FirstName, Password, PhoneNumber) values(@FirstName, @Password, @PhoneNumber);", conn);
			//		cmd.Parameters.AddWithValue("@FirstName", contactName);
			//		cmd.Parameters.AddWithValue("@Password", contactNumber);;
			//		cmd.Parameters.AddWithValue("@PhoneNumber", Password);
			//		cmd.ExecuteNonQuery();

			//	}
			return RedirectToAction("TaskThird");
			//}
			//catch (MySqlException ex)
			//{
			//	return Content("<script language='javascript' type='text/javascript'>alert(ex.ToString());</script>");
			//}

		}






		//public void AuthFun(string contactNumber)
		//{
		//	//string line = "";
		//	//line += "<option>";
		//	//line += "suka";
		//	//line += "</option>";
		//	//ViewBag.H = line;
		//	MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
		//	try
		//	{
		//		if (conn.State == ConnectionState.Closed)
		//		{
		//			conn.Open();
		//			MySqlCommand cmd = new MySqlCommand("select status from zayavka where number = @number", conn);
		//			cmd.Parameters.AddWithValue("@number", contactNumber);
		//			MySqlDataReader reader = cmd.ExecuteReader();
		//			string line = "";
		//			while (reader.Read())
		//			{
		//				string text = Convert.ToString(reader["status"]);
		//				line += text;
		//				line += Environment.NewLine;
		//			}
		//			reader.Close();
		//			ViewBag.H = line;
		//		}
		//	}
		//	catch { Content("<script language='javascript' type='text/javascript'>alert(ex.ToString());</script>"); }
		//}

	}

}
