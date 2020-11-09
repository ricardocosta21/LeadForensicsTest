using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using forensicsTest.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using forensicsTest.Services;
using Microsoft.AspNetCore.Hosting;

namespace forensicsTest.Controllers
{
    public class HomeController : Controller
    {
        public static List<WebsiteModel> WebsitesList { get; set; }

        public static List<Record> RecordsList { get; set; }

        private readonly ICsvParserService _parserService;

        private readonly IWebScraperService _scraperService;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ICsvParserService parserService, IWebScraperService scraperService, IWebHostEnvironment webHostEnvironment)
        {
            _parserService = parserService;
            _scraperService = scraperService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            if (WebsitesList == null)
            {
                WebsitesList = new List<WebsiteModel>();
            }
            return View(WebsitesList);
        }

        [HttpPost("UploadFile")]
        public IActionResult Post(IFormFile file)
        {
            if(file != null)
            {
                using (var stream = file.OpenReadStream())
                {
                    try
                    {
                        string contentPath = _webHostEnvironment.ContentRootPath;
 
                        var websites = new List<WebsiteModel>();

                        var fileName = Path.GetFileName(file.FileName);

                        using (FileStream stream1 = new FileStream(Path.Combine(contentPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(stream1);
                        }

                        WebsitesList = new List<WebsiteModel>();

                        websites = _parserService.ReadCsvFileToWebsiteModel(fileName);

                        WebsitesList.AddRange(websites);
                    }
                    catch
                    {
                        return View();
                    }
                }
            }         

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("ProcessAll")]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessAll()
        {
            try
            {
                var recordsList = new List<Record>();

                if (WebsitesList != null)
                {
                    foreach (var website in WebsitesList)
                    {
                        var record = _scraperService.ScrapeWebPage(website);
                        recordsList.Add(record);
                    } 
                }
                return PartialView("_RecordsPartialView", recordsList);
            }
            catch
            {
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string name)
        {
            try
            {
                var website = WebsitesList.FirstOrDefault(m => m.Name == name);

                WebsitesList.Remove(website);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost("DeleteAll")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAll()
        {
            try
            {
                WebsitesList = new List<WebsiteModel>();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
