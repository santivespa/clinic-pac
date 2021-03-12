using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClinicPacServer.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Nancy.Json;

namespace ClinicPacServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;
        //private const string LocalUrl = "https://localhost:44312/api";
        //private const string Url = "https://localhost:44312/api";
        private readonly HttpClient client = new HttpClient();


        public HomeController(Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            //  var content = new FormUrlEncodedContent(new[]
            //{
            //      new KeyValuePair<string, string>("Email", "asd"),
            //      new KeyValuePair<string, string>("Pass", "asd")
            //  });

            ////login
            // User user = await Login("asd", "asd");
            ////new consultation
            //Consultation c = await CreateConsultation(user, "Ninio Guey", "4536234-56", "Apendice", DateTime.Now);
            //if (c != null)
            //    user.Consultations.Add(c);

            ////
            var url = "https://clinicpactest.santivespa.com/api" + "/Users/Login?Email=" + "drvespa36@hotmail.com" + "&Pass=hola";


            try
            {
                var Scontent = await client.PostAsync(url, null);

                string resultContent = Scontent.Content.ReadAsStringAsync().Result;
                User user = JsonConvert.DeserializeObject<User>(resultContent);

                if (user != null)
                {

                }
                else
                {

                }
            }
            catch (Exception e)
            {

            }
            return View();
        }


        #region api metodods
        //login
      
        //new consultation
      
        #endregion


      
        
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //public IActionResult User()
        //{
        //    return View(_context.Users.FirstOrDefault()) ;
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
