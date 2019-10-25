using System;
using System.Data.Entity; //Include
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        //1st imple : returning Customer objects : part of our domain model
        // - refactoring our domain model breaks api : we need something 'as stable as possible' : DTO
        // - open up security hole in our app (additional data, ...) : DTO to exclude the props that can't be updated
        // => never use or return Customer
        //return Customer ? we need to map it to Dto 1st.
        //modify (update, create) Customer ? we need to map this Dto props back to Customer obj
        // => Use autoMapper to facilitate
        //----------------
        // IHttpActionResult : interface like ActionResult in mvc : use it as return type and replace CustomerDto
        // for ex by convention status must be '201 Created' not '200 OK' for POST
        // 

        private ApplicationDbContext _context; //using Vidly.Models; in IdentityModels : Identity Framework

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/customers
        //IEnumerable<CustomerDto> and use retun ... not return Ok
        public IHttpActionResult GetCustomers(string query = null) //optional param, used for twitter.typeahead
        {
            var customersQuery = _context.Customers
                .Include(c => c.MembershipType); //Eager loading

            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));

            var customersDtos = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>); //AutoMapper.Mapper. No () because we want to pass it as delegate to LINQ select

            return Ok(customersDtos);
        }

        // GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            return
                Ok(Mapper.Map<Customer, CustomerDto>(customer)); // helper method
        }

        // POST /api/customers
        //because create ress : use att [HttpPost] or PostCustomer. The 1st is more robuste
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid) //validate input
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest(); // return class that implement IHttpActionResult


            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            //return customerDto; // now have an id generated in server (after adding into context)
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto); // REST conv we need to return Uri (/api/customers/10) of newly cre ob
        }

        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid) //validate input
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if(customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb); // use returned objet OR pass-it as second arg and you can delete <CustomerDto, Customer>
            /*customerInDb.Name = customer.Name;
            customerInDb.Birthday = customer.Birthday;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;*/

            _context.SaveChanges();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerInDb); //object will be marked ass removed in memory
            _context.SaveChanges();

        }
    }



}
