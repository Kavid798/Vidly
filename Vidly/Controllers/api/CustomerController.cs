using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using System.Web.Routing;
using AutoMapper;
using Vidly.DTO;

namespace Vidly.Controllers.api
{
    public class CustomerController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomerController()
        {
            _context = new ApplicationDbContext();

        }
        //GET/api/customer
        public IEnumerable<CustomerDto> GetCustomers()
        {
            var a= _context.Customers.Include(c=>c.MembershipType).ToList().Select(Mapper.Map<Customer, CustomerDto>);
            return a;
        }
        //GET/api/customer/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }
        [System.Web.Http.HttpPost]
        //POST/api/customer

        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            //throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }
        [System.Web.Http.HttpPut]
        //PUT/api/customer/1
        public void UpdateCustomer(int id, CustomerDto customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            //customerInDb.Name = customer.Name;
            //customerInDb.BirthDate = customer.BirthDate;
            //customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            //customerInDb.MembershipTypeId = customer.MembershipTypeId;
            Mapper.Map(customer, customerInDb);
            _context.SaveChanges();

        }
        [System.Web.Http.HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

        }
    }
}
