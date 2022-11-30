using CustomTagHelper.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;

namespace CustomTagHelper.Controllers
{
    public class CustomerController : Controller
    {
        #region private property
        private readonly string _sessionKey = "customers";
        private readonly IValidator<CustomerViewModel> _validator;
        #endregion
        #region public property
        public List<CustomerViewModel> Customers 
        { 
            get
            {
                if(string.IsNullOrEmpty(HttpContext.Session.GetString(_sessionKey)) )
                {
                    var list = new List<CustomerViewModel>()
                    {
                        new CustomerViewModel()
                        {
                            Id = 1,
                            Firstname = "Eric",
                            Lastname = "Johnson",
                            Username = "Eric.J",
                            BirthDate = DateTime.ParseExact("01/01/1970", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            Email = "eric.johnson@test.com"
                        },
                        new CustomerViewModel()
                        {
                            Id = 2,
                            Firstname = "Carlos",
                            Lastname = "Santana",
                            Username = "Carlos.S",
                            BirthDate = DateTime.ParseExact("02/02/1971", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            Email = "carlos.santana@test.com"
                        }
                    };
                    HttpContext.Session.SetString(_sessionKey, JsonConvert.SerializeObject(list));
                }
                return JsonConvert.DeserializeObject<List<CustomerViewModel>>(HttpContext.Session.GetString(_sessionKey));
            }

            set
            {
                if(value == null)
                {
                    HttpContext.Session.SetString(_sessionKey, JsonConvert.SerializeObject(new List<CustomerViewModel>()));
                }
                else
                {
                    HttpContext.Session.SetString(_sessionKey, JsonConvert.SerializeObject(value));
                }
                
            }
        }
        #endregion

        public CustomerController(IValidator<CustomerViewModel> val) 
        { 
            _validator = val;
        }

        // GET: CustomerController
        public ActionResult Index()
        {
            return View(Customers);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            var model = Customers.Where(x => x.Id == id).FirstOrDefault();
            return View(model);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            var nextId = Customers.Select(x => x.Id).Max() + 1;
            return View(new CustomerViewModel() { Id = nextId });
        }

        // GET: CustomerController/Create
        public ActionResult AjaxCreate()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            try
            {
                //var result = await _validator.ValidateAsync(model);
                //if (!result.IsValid)
                //{
                //    result.AddToModelState(this.ModelState);
                //    return View("Create", model);
                //}

                var list = Customers;
                list.Add(model);
                Customers = list;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // to log:
                throw new Exception("Server error!");
            }
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjaxCreate(CustomerViewModel model)
        {
            try
            {
                var result = await _validator.ValidateAsync(model);
                if (!result.IsValid)
                {
                    result.AddToModelState(this.ModelState);
                    return View("Create", model);
                }

                var list = Customers;
                list.Add(model);
                Customers = list;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // to log:
                throw new Exception("Server error!");
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = Customers.Where(x => x.Id == id).FirstOrDefault();
            return View(model);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerViewModel model)
        {
            try
            {
                //var model = Customers.FirstOrDefault(x => x.Id == id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
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
