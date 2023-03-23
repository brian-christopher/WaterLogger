using System.ComponentModel.DataAnnotations;

namespace WaterLogger.Models
{
    public class DrinkingWaterModel
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage ="Value for {0} must be positive.")]
        public int Quantity { get; set; }
    }
}
