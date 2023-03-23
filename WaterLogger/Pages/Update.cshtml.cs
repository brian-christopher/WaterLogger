using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using WaterLogger.Models;

namespace WaterLogger.Pages
{
    public class UpdateModel : PageModel
    {
		private readonly IConfiguration _configuration;

		public UpdateModel(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[BindProperty]
		public DrinkingWaterModel DrinkingWater { get; set; }

		public async Task OnGetAsync(int id)
		{
			DrinkingWater = await GetByIdAsync(id);
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if(!ModelState.IsValid)
			{
				return Page();
			}

			await using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
			{
				connection.Open();
				var tableCmd = connection.CreateCommand();
				tableCmd.CommandText = $"UPDATE drinking_water SET Date = '{DrinkingWater.Date}', Quantity = '{DrinkingWater.Quantity}' WHERE Id = {DrinkingWater.Id}";

				await tableCmd.ExecuteNonQueryAsync();
			}

			return RedirectToPage("./Index"); 
		}

		private async Task<DrinkingWaterModel> GetByIdAsync([FromForm] int id)
		{
			var drinkingWaterRecord = new DrinkingWaterModel();

			await using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
			{
				connection.Open();
				var tableCmd = connection.CreateCommand();
				tableCmd.CommandText = $"SELECT * FROM drinking_water WHERE Id = {id}";

				var reader = await tableCmd.ExecuteReaderAsync();

				while (reader.Read())
				{
					drinkingWaterRecord.Id = reader.GetInt32(0);
					drinkingWaterRecord.Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentCulture.DateTimeFormat);
					drinkingWaterRecord.Quantity = reader.GetInt32(2);
				}
			}

			return drinkingWaterRecord;
		}
	}
}
