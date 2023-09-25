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
	public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
	{
		private readonly ApplicationDbContext _db;
		public OrderDetailsRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
		public void Update(OrderDetails obj)
		{
			_db.OrderDetails.Update(obj);
		}
	}
}
