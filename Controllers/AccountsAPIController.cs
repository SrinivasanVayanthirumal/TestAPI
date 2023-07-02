using Microsoft.AspNetCore.Mvc;
using System.Net;
using Test.ApplicationCore;
using Test.infrastructure;
using Test.infrastructure.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Test.ApplicationCore.BusinessModels;
using Test.ApplicationCore.Models;
using Test.infrastructure.Repository;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsAPIController : ControllerBase
    {
        public readonly IAccountsRepository AccountsRepository;

        public AccountsAPIController(IAccountsRepository AccountsRep)
        {

            this.AccountsRepository = AccountsRep;

        }

        [Route("SaveAccounts")]
        [HttpPost]
        public IActionResult SaveAccounts(Accounts Accountsdata)
        {
            try
            {

                var result = AccountsRepository.SaveAccounts(Accountsdata);
               
                return Ok(result);
                //  return result.ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("GetAllAccounts")]
        [HttpGet]
        public IActionResult GetAllAccounts()
        {
            List<Accounts> result = new List<Accounts>();
            try
            {
               
                result = AccountsRepository.GetAllAccounts().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
            return Ok(result);
        }

        [Route("GetCustomerAccounts")]
        [HttpPost]
        public IActionResult GetCustomerAccounts(int CustomerId)
        {
            List<SPCustomerAccountsData> result = new List<SPCustomerAccountsData>();
            try
            {
                result = AccountsRepository.GetCustomerAccounts(CustomerId);



            }
            catch (Exception ex)
            {
                return null;
            }
            return Ok(result);
        }

        [Route("DeleteAccountsInformation")]
        [HttpGet]
        public IActionResult DeleteAccountsInformation(string CustomerId)
        {
            RemoteResult<string> result = new RemoteResult<string>();
            try
            {
                result.data = AccountsRepository.DeleteAccountsInformation(CustomerId);

            }
            catch (Exception ex)
            {
                result.SetError(ex);
            }
            return Ok(result);
        }

    }
}
