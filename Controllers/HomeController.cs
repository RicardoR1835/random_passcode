using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using randome_Passcode.Models;
using Microsoft.AspNetCore.Http;

namespace randome_Passcode.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("Count") == null)
            {
                HttpContext.Session.SetInt32("Count", 0);
            }
            ViewBag.Word = HttpContext.Session.GetString("passcode");
            ViewBag.Count = HttpContext.Session.GetInt32("Count");
            return View();
        }

        [HttpPost("random_word")]
        public IActionResult Generate()
        {
            int? Count = HttpContext.Session.GetInt32("Count");
            Count++;
            HttpContext.Session.SetInt32("Count", Convert.ToInt32(Count));
            string PassCodeLetters = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            Random rand = new Random();
            char[] options = new char[14];
            for(int i = 0; i < 14; i++)
            {
                options[i] = PassCodeLetters[rand.Next(PassCodeLetters.Length)];
            }
            string random_word = new string(options);
            HttpContext.Session.SetString("passcode", random_word);
            return RedirectToAction("Index");
        }

    }
}
