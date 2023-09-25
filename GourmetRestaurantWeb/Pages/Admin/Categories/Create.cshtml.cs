using GourmetRestaurant.DataAccess.Data;
using GourmetRestaurant.DataAccess.Repository.IRepository;
using GourmetRestaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GourmetRestaurantWeb.Pages.Admin.Categories
{
	[BindProperties]
	public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public Category Category { get; set; }

        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if(Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name", "Name And The Display Order Cannot Be The Same!");
            }
            if(ModelState.IsValid)
            { 
                _unitOfWork.Category.Add(Category);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToPage("Index");
			}
            return Page();
		}
    }
}
