using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PursueBank.Business;
using PursueBank.Core.Models;

namespace PursueBank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedgerController : ControllerBase
    {
        private readonly IAccountManager _accountManager;

        public LedgerController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetLedger(int accountId, DateTime startDate)
        {
            try
            {
                var acctTrans = await Task.Run(() => _accountManager.GetAllTransactionByAccountId(accountId, startDate));

                return Ok(acctTrans);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RequestPayment(PaymentRequest request)
        {
            try
            {
                var acctTrans = await Task.Run(() => _accountManager.RequestPayment(request));

                return Ok(acctTrans);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}