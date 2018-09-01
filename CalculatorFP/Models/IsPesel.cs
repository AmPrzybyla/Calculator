using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CalculatorFP.ViewModels;

namespace CalculatorFP.Models
{
    public class IsPesel : ValidationAttribute
    {
        private ApplicationDbContext _context;
        public IsPesel()
        {
            _context = new ApplicationDbContext();
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var vote = (VoteViewModel) validationContext.ObjectInstance;


            //chceck length of personal umber
            if (vote.PersonalNumber == null)
                return new ValidationResult(" Must Have 11 Chars");

            if (vote.PersonalNumber.Count()!=11)
            {
                return new ValidationResult(" Must Have 11 Chars");
            }

            //check if voted
            if(_context.Votes.Any(c=>c.PersonalNumber==vote.PersonalNumber))
            {
                return new ValidationResult("You Voted");

            }

            //check if is blocked
            if (_context.Disalloweds.Any(c=>c.Pesel==vote.PersonalNumber))
            {
                return new ValidationResult("You Can't Voted");

            }

            //check if is adult
            if (!Have18(vote.PersonalNumber))
            {
                return new ValidationResult("You Are To Young");      
            }

            int sum = (int.Parse(vote.PersonalNumber[0].ToString()) * 1) + (int.Parse(vote.PersonalNumber[1].ToString()) * 3) + (int.Parse(vote.PersonalNumber[2].ToString()) * 7 );
            sum += (int.Parse(vote.PersonalNumber[3].ToString()) * 9) + (int.Parse(vote.PersonalNumber[4].ToString()) * 1) + (int.Parse(vote.PersonalNumber[5].ToString()) * 3) ;
            sum += (int.Parse(vote.PersonalNumber[6].ToString()) * 7) + (int.Parse(vote.PersonalNumber[7].ToString()) * 9) + (int.Parse(vote.PersonalNumber[8].ToString())* 1) + (int.Parse(vote.PersonalNumber[9].ToString()) * 3);
            

            return (10-(sum%10)  == int.Parse(vote.PersonalNumber[10].ToString()) ? ValidationResult.Success : new ValidationResult("False Personal Number"));
        }

        private bool Have18(string personalNumber)
        {
            int year=10*int.Parse(personalNumber[0].ToString());
            year += int.Parse(personalNumber[1].ToString());
            int month = 10 * int.Parse(personalNumber[2].ToString());
            month += int.Parse(personalNumber[3].ToString());
            int day = 10 * int.Parse(personalNumber[4].ToString());
            day += int.Parse(personalNumber[5].ToString());
            if (month > 80)
            {
                year += 1800;
                month -= 80;
            }
            else if (month > 20 && month < 33)
            {
                year += 2000;
                month -= 20;
            }
            else if (month > 0 && month < 13)
            {
                year += 1900;
            }

            if (DateTime.Now.Year - year > 18)
                return true;
            else if (DateTime.Now.Year - year == 18)
            {
                if (DateTime.Now.Month - month > 0)
                    return true;
                else if (DateTime.Now.Day > 0)
                    return true;
            }
            else
                return false;

            return false;
        }
    }
}