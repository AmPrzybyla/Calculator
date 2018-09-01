using CalculatorFP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CalculatorFP.ViewModels;

namespace CalculatorFP.Controllers.Api
{
    public class PartyController : ApiController
    {
        private ApplicationDbContext _context;

        public PartyController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetPartyList()
        {
            var partyList = _context.Votes.GroupBy(v => v.Candidate.PartyType.Name).Select(g => new PartyListViewModel
            {
                PartyName = g.Key.ToString(),
                PartyCount=g.Count()
            }).OrderBy(p=>p.PartyCount);

            return Ok(partyList);
        }
    }
}
