using AutoMapper;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Mapper;
using Web.Models;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderController(IUnitOfWork uow, IMapper p)
        {
            this._unitOfWork = uow;
            this._mapper = p;
        }

        // GET: OrderController
        public async Task<ActionResult> Index()
        {
            var orders = await _unitOfWork.OrderRepository.GetMany(c => !string.IsNullOrEmpty(c.CreatedBy));
            var modelViewList = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                var model = _mapper.Map<OrderViewModel>(order);
                modelViewList.Add(model);
            }
             
            return View(modelViewList);
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public async Task<ActionResult> Create()
        {
            var modelView = new OrderViewModel()
            {
                OrderDate = DateTime.Now,
                OrderBy = "Test"
            };
            return View(modelView);
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrderViewModel model)
        {
            try
            {
                model.Status = Core.Enums.OrderStatus.Confirmed;
                //var newOrder = _mapper.Map<Order>(model);
                var newOrder = new Order()
                {
                    CreatedDateTime = DateTime.Now,
                    CreatedBy = "Test",
                    Status = Core.Enums.OrderStatus.Pending,
                };

                var order = await _unitOfWork.OrderRepository.AddAsync(newOrder);
                order.AddToEvents();
                await _unitOfWork.CommitAndNotifyAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
