using CalculatorFP.Models;
using CalculatorFP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CalculatorFP.Controllers.Api
{
    public class CandidateController : ApiController
    {
        private ApplicationDbContext _context;

        public CandidateController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetPartyList()
        {
            var partyList = _context.Votes.GroupBy(v => v.Candidate.Name).Select(g => new CandidateListViewModel
            {
                CandidateName = g.Key.ToString(),
                CandidateCount = g.Count()
            }).OrderBy(c=>c.CandidateName);

            return Ok(partyList);
        }
    }
}
