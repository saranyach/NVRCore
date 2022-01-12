using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NVRCore.Models;
using NVRCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NVRCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {


        public CustomerRepository customerRepository;
        public CustomerController()
        {
            this.customerRepository = new CustomerRepository();
        }
       
        [HttpGet]
        public List<Customer> Get()
        {
            return customerRepository.GetCustomers();

        }

        [HttpPost]
        public HttpResponseMessage Post(Customer customer)
        {
            HttpResponseMessage responseMessage;
            if (ModelState.IsValid)
            {
                try
                {
                    customerRepository.SaveCustomer(customer);
                    responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Customer " + customer.FirstName + " " + customer.LastName + " added.")
                    };
                    return responseMessage;
                }
                catch (Exception)
                {
                    responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    return responseMessage;
                }
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);



        }
    }
}
