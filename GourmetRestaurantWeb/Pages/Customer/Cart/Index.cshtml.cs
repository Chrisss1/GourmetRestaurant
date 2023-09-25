using GourmetRestaurant.DataAccess.Repository.IRepository;
using GourmetRestaurant.Models;
using GourmetRestaurant.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace GourmetRestaurantWeb.Pages.Customer.Cart
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }

        public double CartTotal { get; set; }

        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CartTotal = 0;
        }
        public void OnGet()
        {
            //retrieve user id of the logged in user, after we get the complete list of shoppingcart list
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if(claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value,
                    includeProperties:"MenuItem,MenuItem.FoodType,MenuItem.Category");
            }

            foreach(var CartItem in ShoppingCartList)
            {
                CartTotal += (CartItem.MenuItem.Price * CartItem.Count);
            }
        }
        //Plus --> asp-page-handler name, cartId --> asp-route-(CartId)
        public IActionResult OnPostPlus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.IncrementCount(cart, 1);
            return RedirectToPage("/Customer/Cart/Index");
        }

        public IActionResult OnPostMinus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            if(cart.Count == 1)
            {
                //session
				var count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count - 1;

				_unitOfWork.ShoppingCart.Remove(cart);
                _unitOfWork.Save();
				//session
				HttpContext.Session.SetInt32(SD.SessionCart, count);
			}
            else
            {
                _unitOfWork.ShoppingCart.DecrementCount(cart, 1);
            }
            return RedirectToPage("/Customer/Cart/Index");
        }

        public IActionResult OnPostRemove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            //session
			var count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count-1;

			_unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
			//session
			HttpContext.Session.SetInt32(SD.SessionCart, count);
			return RedirectToPage("/Customer/Cart/Index");
        }
    }
}
