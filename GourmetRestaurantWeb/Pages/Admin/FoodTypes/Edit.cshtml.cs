using GourmetRestaurant.DataAccess.Data;
using GourmetRestaurant.DataAccess.Repository.IRepository;
using GourmetRestaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GourmetRestaurantWeb.Pages.Admin.FoodTypes
{
	[BindProperties]
	public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public FoodType FoodType { get; set; }

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            //we need id to edit category
            FoodType = _unitOfWork.FoodType.GetFirstOrDefault(u => u.Id == id);
            
            //Category = _db.Category.FirstOrDefault(u=>u.Id==id);
            //Category = _db.Category.SingleOrDefault(u=>u.Id==id);
            //Category = _db.Category.Where(u=>u.Id==id).FirstOrDefault();

        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            { 
                _unitOfWork.FoodType.Update(FoodType);
                _unitOfWork.Save();
				TempData["success"] = "FoodType Updated Successfully";
				return RedirectToPage("Index");
			}
            return Page();
		}
    }
}
