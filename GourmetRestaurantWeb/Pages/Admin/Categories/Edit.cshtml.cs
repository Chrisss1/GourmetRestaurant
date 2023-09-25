using GourmetRestaurant.DataAccess.Data;
using GourmetRestaurant.DataAccess.Repository.IRepository;
using GourmetRestaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GourmetRestaurantWeb.Pages.Admin.Categories
{
	[BindProperties]
	public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public Category Category { get; set; }

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            //we need id to edit category
            Category = _unitOfWork.Category.GetFirstOrDefault(u=>u.Id==id);
            
            //Category = _db.Category.FirstOrDefault(u=>u.Id==id);
            //Category = _db.Category.SingleOrDefault(u=>u.Id==id);
            //Category = _db.Category.Where(u=>u.Id==id).FirstOrDefault();

        }

        public async Task<IActionResult> OnPost()
        {
            if(Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name", "Name And The Display Order Cannot Be The Same!");
            }
            if(ModelState.IsValid)
            { 
                _unitOfWork.Category.Update(Category);
                _unitOfWork.Save();
				TempData["success"] = "Category Updated Successfully";
				return RedirectToPage("Index");
			}
            return Page();
		}
    }
}
