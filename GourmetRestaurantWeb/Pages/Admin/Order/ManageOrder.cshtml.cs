using GourmetRestaurant.DataAccess.Repository.IRepository;
using GourmetRestaurant.Models;
using GourmetRestaurant.Models.ViewModel;
using GourmetRestaurant.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GourmetRestaurantWeb.Pages.Admin.Order
{
    [Authorize(Roles = $"{SD.KitchenRole},{SD.ManagerRole}")]
    public class ManageOrderModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public List<OrderDetailVM> OrderDetailVM { get; set; }
        public ManageOrderModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            //populate orderdetailvm
            OrderDetailVM = new();

            List<OrderHeader> orderHeaders = _unitOfWork.OrderHeader.GetAll(u => u.Status == SD.StatusSubmitted ||
            u.Status == SD.StatusInProcess).ToList();

            foreach(OrderHeader item in orderHeaders)
            {
                OrderDetailVM individual = new OrderDetailVM()
                {
                    OrderHeader = item,
                    OrderDetails = _unitOfWork.OrderDetails.GetAll(u => u.OrderId == item.Id).ToList()
                };
                OrderDetailVM.Add(individual);
            }
        }
        //manage order buttons
        public IActionResult OnPostOrderInProcess(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusInProcess);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }
        //manage order buttons
        public IActionResult OnPostOrderReady(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusReady);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }
        //manage order buttons
        public IActionResult OnPostOrderCancel(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusCancelled);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }
    }
}
