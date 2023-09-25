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
	public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
	{
		private readonly ApplicationDbContext _db;
		public OrderHeaderRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
		public void Update(OrderHeader obj)
		{
			_db.OrderHeader.Update(obj);
		}

        public void UpdateStatus(int id, string status)
        {
            var orderFromDb = _db.OrderHeader.FirstOrDefault(u => u.Id == id);
			if (orderFromDb != null)
			{
				orderFromDb.Status = status;
			}
        }
    }
}
