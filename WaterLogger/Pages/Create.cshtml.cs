using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterLogger.Models;

namespace WaterLogger.Pages
{
	public class CreateModel : PageModel
	{
		private readonly IConfiguration _configuration;

		[BindProperty]
		public DrinkingWaterModel DrinkingWater { get; set; }

		public IEnumerable<KeyValuePair<string, int>> MeasuresList { get; set; }

		public CreateModel(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void OnGet()
		{
			MeasuresList = EnumMethods.GetSelectList<Measures>();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				MeasuresList = EnumMethods.GetSelectList<Measures>();
				return Page();
			}

			await using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
			{
				connection.Open();
				var tableCmd = connection.CreateCommand();
				tableCmd.CommandText = @$"INSERT INTO drinking_water(date, quantity, Measure) VALUES('{DrinkingWater.Date}','{DrinkingWater.Quantity}', '{(int)DrinkingWater.Measure}')";

				await tableCmd.ExecuteNonQueryAsync();
			}

			return RedirectToPage("./Index");
		}
	}
}
