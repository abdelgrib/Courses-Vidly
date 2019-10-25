using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {

        private ApplicationDbContext _context;

        //ctor + Tab. To init _context
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //This DbContext is a disposable object so we need to properly dispose this object by overring dispo method of base ctrl class
        protected override void Dispose(bool disposing)
        {
            //base.Dispose(disposing);
            _context.Dispose();
        }

        /*List<Customer> customers = new List<Customer>
        {
            new Customer { Name = "John Smith", Id = 1 },
            new Customer { Name = "Mary Williams", Id = 2 }
        };*/

        public ActionResult New() // ce qui va s'afficher dans url
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(), // n'a pas besoin des informations Customer (New) mais juste pour summury pour avoir un id initialisé à 0 (valeur par déf pour byte) et pas null car ça pose un prob de validation
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", viewModel); // overriding default behavior 
        }

        public ActionResult Edit(int Id) 
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == Id); // return null if not found
            if (customer == null)
                return HttpNotFound();
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
        }

        [HttpPost] // resquest data. Best practice: when action modify data should not be acc by GET
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer) //can use CustomerFormViewModel but asp mvc can understant and map req data to this obj because are prefixed with Customer
        {
            if(!ModelState.IsValid) // to exploit annotations to done server side validation
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel); // return same view if problem
            }

            if (customer.Id == 0)
                _context.Customers.Add(customer); // just in memory
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id); // throw exception if not found, we don't handle case when not found
                customerInDb.Name = customer.Name; // better than TryUpdateModel because secu : it update all fields. We specify params but 'strings'
                customerInDb.Birthday = customer.Birthday;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            _context.SaveChanges(); // to persist changes (create and apply SQL cmd)
            return RedirectToAction("AllCustomers","Customers");
        }

        public ActionResult AllCustomers()
        {

            /*var customersModel = new CustomersList
            {
                Customers = customers 
            };*/

            //var customers = _context.Customers or .ToList();; // differed execution !
            //var customers = _context.Customers.Include(c => c.MembershipType).ToList(); //Eager load : load object not just FK. Expression that det the target property in Include method from 'using .Data.Entity'
            //return View(customers); //no server side rendering
            return View();
        }

        //mvc4action + Tab
        public ActionResult Details(int Id)
        {
            /*var customer = new Customer();
            foreach(var c in customers)
            {
                if (c.Id == Id) customer = c;      
            }
            if (customer.Name != null)
                return View(customer);
            else
                return HttpNotFound(); 
            */

            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == Id);

            if (customer == null)
                return HttpNotFound();
            return View(customer);
        }
    }
}