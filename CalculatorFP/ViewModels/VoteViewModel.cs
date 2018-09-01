using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CalculatorFP.Models;

namespace CalculatorFP.ViewModels
{
    public class VoteViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Pesel")]
        [IsPesel]
        public string PersonalNumber { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int CandidateId { get; set; }

        public bool SpoiledVote { get; set; }

        public bool IsVoted { get; set; }

        //public Vote Vote { get; set; }

        public IEnumerable<Candidate> Candidates { get; set; }


        public int[] Selected { get; set; }
    }
}