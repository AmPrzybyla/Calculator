using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CalculatorFP.Models;

namespace CalculatorFP.ViewModels
{
    public class VoteFormViewModel
    {
      //  public IEnumerable<PartyType> PartyTypes { get; set; }
        public IEnumerable<Candidate> Candidates { get; set; }
        public Vote Vote { get; set; }
    }
}