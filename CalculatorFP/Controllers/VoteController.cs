using CalculatorFP.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CalculatorFP.ViewModels;
using Rotativa;
using System.Text;
using System.Data;
using Jitbit.Utils;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Threading;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Borders;

namespace CalculatorFP.Controllers
{
    [Authorize]
    public class VoteController : Controller
    {
        private ApplicationDbContext _context;

        public VoteController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Vote
        public ActionResult Vote()
        {
            var candidate = _context.Candidates.Include(c => c.PartyType).ToList();

            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = _context.Users.FirstOrDefault(x => x.Id == currentUserId);
            var viewModel = new VoteViewModel
            {

                Candidates = candidate,
                PersonalNumber = currentUser.PersonalNumber,
                Name=currentUser.Name,
                Surname=currentUser.Surname
            };

            return View(viewModel);
        }

        [AllowAnonymous]
        public ActionResult PartyChart()
        {
            var viewModel = _context.Votes
                .Where(v=>v.SpoiledVote==false)
                .GroupBy(v => v.Candidate.PartyType.Name)
                .Select(g => new PartyListViewModel
                {
                    PartyName = g.Key.ToString(),
                    PartyCount = g.Count()
                })
                .OrderBy(p => p.PartyName).ToList();

            return View(viewModel);
        }

        [AllowAnonymous]
        public ActionResult Charts()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult CandidateChart()
        {

            var viewModel = _context.Votes
                .Where(v=>v.SpoiledVote==false)
                .GroupBy(v => v.Candidate.Name)
                .Select(g => new CandidateListViewModel
            {
                CandidateName = g.Key.ToString(),
                CandidateCount = g.Count()
            }).OrderBy(c => c.CandidateName).ToList();
            return View(viewModel);
        }



        public ActionResult List()
        {
            
            return View();
        }

        

        [HttpPost]
        public ActionResult Save(VoteViewModel voteModel)
        {
            var vote = new Vote();
            if (!ModelState.IsValid)
            {
                var viewModel = new VoteViewModel
                {
                    Candidates = _context.Candidates.Include(c => c.PartyType).ToList(),
                    Name = voteModel.Name,
                    CandidateId = voteModel.CandidateId,
                    PersonalNumber = voteModel.PersonalNumber,
                    Surname = voteModel.Surname

                };

                return View("Vote", viewModel);
            }
            if (voteModel.Selected == null || voteModel.Selected.Count()>1)
            {
                vote = new Vote
                {
                    Name = voteModel.Name,
                    Surname = voteModel.Surname,
                    PersonalNumber =voteModel.PersonalNumber,
                    SpoiledVote = true
                };
            }


            else
            {
                vote = new Vote
                {
                    Name = voteModel.Name,
                    Surname = voteModel.Surname,
                    PersonalNumber = voteModel.PersonalNumber,
                    CandidateId = voteModel.Selected[0]




                };
            }
            _context.Votes.Add(vote);
            _context.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult GeneratePdf()
        {
            MemoryStream stream = new MemoryStream();

            PdfWriter writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            var candidateLists = _context.Votes.GroupBy(v => v.Candidate.Name).Select(g => new
            {
                CandidateName = g.Key.ToString(),
                CandidateCount = g.Count()
            }).OrderByDescending(c => c.CandidateCount).ToList();


            Table candidateTable = new Table(2, true);

            candidateTable.AddCell("Candidate");
            candidateTable.AddCell("Votes");


            foreach (var candidate in candidateLists)
            {
                candidateTable.AddCell(new Paragraph(candidate.CandidateName));
                candidateTable.AddCell(new Paragraph(candidate.CandidateCount.ToString()));
            }

            document.Add(candidateTable);


            document.Add(new Paragraph("\n\n"));

            var partyLists = _context.Votes.GroupBy(v => v.Candidate.PartyType.Name).Select(g => new
            {
                PartyName = g.Key.ToString(),
                PartyCount = g.Count()
            }).OrderByDescending(c => c.PartyCount).ToList();


            candidateTable.AddCell("Party Name");
            candidateTable.AddCell("Votes");
            foreach (var party in partyLists)
            {
                candidateTable.AddCell(new Paragraph(party.PartyName));
                candidateTable.AddCell(new Paragraph(party.PartyCount.ToString())).SetBorderBottom(new SolidBorder(4));

            }
            document.Add(candidateTable);



            document.Close();

            return File(stream.ToArray(), "application/pdf", "test.pdf");
        }


        public ActionResult ExportCSV()
        {


            var candidateLists = _context.Votes.GroupBy(v => v.Candidate.Name).Select(g => new 
            {
                CandidateName = g.Key.ToString(),
                CandidateCount = g.Count()
            }).OrderByDescending(c => c.CandidateCount).ToList();

            var partyLists = _context.Votes.GroupBy(v => v.Candidate.PartyType.Name).Select(g => new
            {
                PartyName = g.Key.ToString(),
                PartyCount = g.Count()
            }).OrderByDescending(c => c.PartyCount).ToList();

            var myExport = new CsvExport();

            foreach (var item in candidateLists)
            {
                myExport.AddRow();
                myExport["Candidate"] = item.CandidateName;
                myExport["Votes"] = item.CandidateCount;
            }
            myExport.AddRow();
            myExport.AddRow();
            foreach (var item in partyLists)
            {
                myExport.AddRow();
                myExport["Candidate"] = item.PartyName;
                myExport["Votes"] = item.PartyCount;
            }
        

            return File(myExport.ExportToBytes(), "text/csv", "results.csv");
           
        }


        public void ClearDisallowed()
        {
            var disallowedList = _context.Disalloweds.Where(d => d.Id > 0).ToList();
            foreach (var d in disallowedList)
                _context.Disalloweds.Remove(d);
        }

        public ActionResult RefreshDisallowed()
        {
            ClearDisallowed();
            WebClient c = new WebClient();
            var data = c.DownloadString("http://webtask.future-processing.com:8069/blocked");
            JObject ob = JObject.Parse(data);

            var pesel = ob["disallowed"]["person"].Select(p => p["pesel"]).ToList();

            foreach (var p in pesel)
            {
                var disallowed = new Disallowed
                {
                    Pesel = p.ToString()
                };
                _context.Disalloweds.Add(disallowed);
            }

            _context.SaveChanges();

            return RedirectToAction("List");
        }


    }
}