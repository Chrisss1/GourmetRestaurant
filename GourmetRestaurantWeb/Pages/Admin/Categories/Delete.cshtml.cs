using GourmetRestaurant.DataAccess.Data;
using GourmetRestaurant.DataAccess.Repository.IRepository;
using GourmetRestaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GourmetRestaurantWeb.Pages.Admin.Categories
{
	[BindProperties]
	public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public Category Category { get; set; }

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            //we need id to edit category
            Category = _unitOfWork.Category.GetFirstOrDefault(u=>u.Id == id);

            //Category = _db.Category.FirstOrDefault(u=>u.Id==id);
            //Category = _db.Category.SingleOrDefault(u=>u.Id==id);
            //Category = _db.Category.Where(u=>u.Id==id).FirstOrDefault();

        }

        public async Task<IActionResult> OnPost()
        {
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == Category.Id);
			if (categoryFromDb != null)
            {
			    _unitOfWork.Category.Remove(categoryFromDb);
			    _unitOfWork.Save();
				TempData["success"] = "Category Deleted Successfully";
				return RedirectToPage("Index");
			}
			return Page();
		}
    }
}
