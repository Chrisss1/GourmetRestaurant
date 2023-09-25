using GourmetRestaurant.DataAccess.Data;
using GourmetRestaurant.DataAccess.Repository.IRepository;
using GourmetRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetRestaurant.DataAccess.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly ApplicationDbContext _db;
		public CategoryRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
		public void Update(Category category)
		{
			var objFromDb = _db.Category.FirstOrDefault(u => u.Id == category.Id);
			objFromDb.Name = category.Name;
			objFromDb.DisplayOrder = category.DisplayOrder;
		}
	}
}
