using CalculatorFP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CalculatorFP.Controllers.Api
{
    public class VoteController : ApiController
    {
        private ApplicationDbContext _context;

        public VoteController()
        {
            _context = new ApplicationDbContext();
        }

       // public IHttpActionResult Get Votes
    }
}
