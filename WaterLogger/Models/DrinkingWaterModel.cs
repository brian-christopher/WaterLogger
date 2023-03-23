using System.ComponentModel.DataAnnotations;

namespace WaterLogger.Models
{
	public class DrinkingWaterModel
	{
		public int Id { get; set; }
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }

		[Range(0.0, double.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
		public double Quantity { get; set; }

		[EnumDataType(typeof(Measures))]
		public Measures Measure { get; set; }
	}
}
