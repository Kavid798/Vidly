using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModel;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        private ApplicationDbContext _context;
        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //[NonAction]
        //public List<CustomerDto> GetCustomers()
        //{
        //    List<CustomerDTO> mvc = new List<CustomerDTO>
        //    {
        //        new CustomerDTO{Id=1,Name="Kappaan"},
        //        new CustomerDTO{Id=2,Name="Castle"},
        //        new CustomerDTO{Id=3,Name="Sheldon"},
        //        new CustomerDTO{Id=4,Name="Viji"}
        //    };
        //    return mvc;
        //}
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var membershiptype = _context.MembershipTypes.ToList();
                var viewModel = new CustomerFormViewModel
                {
                    MembershipTypes = membershiptype
                };
                return View("CustomerForm", viewModel);
            }
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var cust = _context.Customers.FirstOrDefault(c => c.Id == customer.Id);
                cust.Name = customer.Name;
                cust.MembershipType = customer.MembershipType;
                cust.BirthDate = customer.BirthDate;
                cust.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;

            }
            _context.SaveChanges();
            return RedirectToAction("DisplayMovie", "Customer");
        }
        public ActionResult DisplayMovie()
        {

            //var customer = _context.Customers.Include(c => c.MembershipType).ToList();
            //return View(customer);
            return View();
        }
        public ActionResult Content(int id)
        {
            var customers = _context.Customers.Include(c => c.MembershipType).FirstOrDefault(c => c.Id == id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);

        }
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
        }

    }
}