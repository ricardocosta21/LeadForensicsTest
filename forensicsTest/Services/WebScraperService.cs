using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using forensicsTest.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace forensicsTest.Services
{
    public class WebScraperService : IWebScraperService
    {
		public Record ScrapeWebPage(WebsiteModel ws)
        {
            try
            {
				var getHtmlWeb = new HtmlWeb();

				Stopwatch timer = new Stopwatch();

				timer.Start();

				var noSpacesStr = ws.Website.Replace(" ", String.Empty);

				var document = getHtmlWeb.Load("http://" + noSpacesStr);

				var nodes = document.DocumentNode.SelectNodes("//*[text()[contains(., 'www.google-analytics.com')]]");

				timer.Stop();

				if (nodes != null)
				{
					var record = new Record(ws.Name, ws.Website, true, timer.Elapsed.TotalSeconds);
					return record;
				}
			}
			catch(Exception ex)
            {
				Console.WriteLine(ex);
            }			
			
            return null;
        }
    }
}
