using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NVRCore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NVRCore.Services
{
    public class CustomerRepository
    {
        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                //Remove whitespaces to check if the file is an empty json file
                string json = String.Concat(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Services\Data\Customers.json").Where(c => !Char.IsWhiteSpace(c))); 
                if (json.Length > 2)
                {
                    var token = JToken.Parse(json);
                    if (token is JArray)
                    {
                        customers = token.ToObject<List<Customer>>();
                    }
                    else if (token is JObject)//This runs if the file only has 1 old customer
                    {
                        customers.Add(token.ToObject<Customer>());
                    }
                    return customers;
                }
                return customers;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveCustomer(Customer customer)
        {
            try
            {
                List<Customer> customers = new List<Customer>();
                string newJson = string.Empty;
                string oldJson = String.Concat(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Services\Data\Customers.json").Where(c => !Char.IsWhiteSpace(c)));
                if (oldJson.Length > 2)
                {
                    var token = JToken.Parse(oldJson);
                    if (token is JArray) //Need this to check if the file has >1 customer
                    {
                        customers = token.ToObject<List<Customer>>();                        
                    }
                    else if (token is JObject)
                    {
                        customers.Add(token.ToObject<Customer>());                       
                    }
                    customers.Add(customer);
                    newJson = JsonConvert.SerializeObject(customers);
                }
                else
                {
                    newJson = JsonConvert.SerializeObject(customer);
                }
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Services\Data\Customers.json", newJson);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
