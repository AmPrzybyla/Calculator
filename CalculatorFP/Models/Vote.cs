using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CalculatorFP.Models
{
    public class Vote
    {
        public int Id { get; set; }

        [Display(Name ="Pesel")]
        public string PersonalNumber { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int? CandidateId { get; set; }

        public Candidate Candidate { get; set; }

        public bool SpoiledVote { get; set; }

        public bool IsVoted { get; set; }
    }
}