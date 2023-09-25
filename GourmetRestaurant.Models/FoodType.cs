using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GourmetRestaurant.Models
{
	public class FoodType
	{
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } 
    }
}
