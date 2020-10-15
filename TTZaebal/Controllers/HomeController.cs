using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceReference1;
using ServiceReference2;
using TTZaebal.Models;
using Microsoft.EntityFrameworkCore;

namespace TTZaebal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            string intrestingDate = "000";
            if (!Request.Cookies.ContainsKey("time"))
            {
                Response.Cookies.Append("time", DateTime.Today.ToString());
            }
            else
            {
                intrestingDate = Request.Cookies["time"];
            }

            var Client = new WebSiteServiceClient();
            var value = await Client.GetTimetableFRAsync("ГЭ20-02Б");

            var ourWeek = new Week(value);
            ourWeek.WeekCreater(intrestingDate);
            return View(ourWeek);
        }
        public IActionResult perfomance()
        {
            perfomanceSelector selector = new perfomanceSelector();
            var asd = selector.getYearSel();

            return View(selector);
        }
        [HttpGet]
        public async Task<IActionResult> getPerfomanceTable(string year, string session)
        {
            var Client = new ServiceReference2.ServiceStudentsClient();
            var value = await Client.GetGradesStudentIdAsync(sessionYear: year, sessionName: session, idStudents: 541618975);
            var grades = new perfomanceClass(value);

            return PartialView(grades);
        }

        //Hjuey2020
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
