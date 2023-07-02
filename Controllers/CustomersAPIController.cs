using Microsoft.AspNetCore.Mvc;
using System.Net;
using Test.ApplicationCore;
using Test.infrastructure;
using Test.infrastructure.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Test.ApplicationCore.BusinessModels;
using Test.ApplicationCore.Models;
using Microsoft.Extensions.Caching;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersAPIController : ControllerBase
    {
        public readonly ICustomerRespository CustomerRespository;

       
       

        public CustomersAPIController(ICustomerRespository CustomerRep)
        {
            this.CustomerRespository = CustomerRep;
        }

       

    
  

        [Route("SaveCustomers")]
        [HttpPost]
        public IActionResult SaveCustomers(Customers customerdata)
        {
            try
            {
                var result = CustomerRespository.SaveCustomers(customerdata);
                if (result == "Customer does not exist")
                    return BadRequest();

                return Ok(result);
               
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       
        [Route("GetAllCustomers")]
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            List<Customers> result = new List<Customers>();
            try
            {
                result = CustomerRespository.GetAllCustomers().ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return null;
                
            }
            return Ok(result);
        }

        [Route("GetCustomers")]
        [HttpPost]
        public IActionResult GetCustomers(int CustomerId)
        {
            RemoteResult<Customers> result = new RemoteResult<Customers>();
            try
            {
                result.data = CustomerRespository.GetCustomers(CustomerId);
                return result.data == null ? BadRequest() : Ok(result.data);

            }
            catch (Exception ex)
            {
                result.SetError(ex);
            }
            return result.data == null ? BadRequest() : Ok(result.data);
        }

       

        [Route("GetAllCustomersPaging")]
        [HttpGet]
        public IActionResult GetAllCustomersPaging(int PageNumber,int PageSize)
        {
            List<SPCustomerListPaging> result = new List<SPCustomerListPaging>();
            try
            {
                result = CustomerRespository.GetAllCustomersPaging(PageNumber, PageSize).ToList();


            }
            catch (Exception ex)
            {
                return null;
            }
            return Ok(result);
        }

        [Route("DeleteCustomerInformation")]
        [HttpGet]
        public IActionResult DeleteCustomerInformation(string UserId)
        {
            RemoteResult<string> result = new RemoteResult<string>();
            try
            {
                // SheUserInfo SheSafetyUsers = JsonConvert.DeserializeObject<SheUserInfo>(WebUtility.UrlDecode(safetyusers));
                result.data = CustomerRespository.DeleteCustomerInformation(UserId);
                return result.data == null ? BadRequest() : Ok(result.data);
            }
            catch (Exception ex)
            {
                result.SetError(ex);
            }
            return Ok(result);
        }



    }
}
