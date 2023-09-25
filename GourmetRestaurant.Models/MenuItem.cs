using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GourmetRestaurant.Models
{
	public class MenuItem
	{
		[Key]
        public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

        public string Image { get; set; }

		[Range(0.00, 1000.00, ErrorMessage = "Price Should Be Between $1 and $1000")]
		public double Price { get; set; }

		[DisplayName("Food Type")]
        public int FoodTypeId { get; set; }

		[ForeignKey("FoodTypeId")]
		public FoodType FoodType { get; set; }

		[DisplayName("Category")]
		public int CategoryId { get; set; }

		//needless to use ForeignKey attribute
		public Category Category { get; set; }

    }
}
