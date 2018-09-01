using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculatorFP.Models
{
    public class Candidate
    {
        public int Id { get; set; }

        public bool IsSelected { get; set; }

        public string Name { get; set; }

        public PartyType PartyType { get; set; }

        public int PartyTypeId { get; set; }
    }
}