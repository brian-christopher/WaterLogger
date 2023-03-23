using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using WaterLogger.Models;

namespace WaterLogger.Pages
{
	public class IndexModel : PageModel
	{

		private readonly IConfiguration _configuration;

		public IndexModel(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public List<DrinkingWaterModel> Records { get; set; }

		public async Task OnGet()
		{
			Records = await GetAllRecords();
			ViewData["Total"] = Records.Sum(x => x.Quantity);
		}

		private async Task<List<DrinkingWaterModel>> GetAllRecords()
		{
			await using var connection = 
				new SqliteConnection(_configuration.GetConnectionString("ConnectionString"));

			connection.Open();

			var tableCmd = connection.CreateCommand();
			tableCmd.CommandText = $"SELECT * FROM drinking_water";

			var tableData = new List<DrinkingWaterModel>();
			var reader = await tableCmd.ExecuteReaderAsync();

			while (reader.Read())
			{
				var data = new DrinkingWaterModel
				{
					Id = reader.GetInt32(0),
					Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentCulture.DateTimeFormat),
					Quantity = reader.GetInt32(2),
					Measure = (Measures)reader.GetInt32(3)
				};

				tableData.Add(data);
			}

			return tableData;
		}
	}
}