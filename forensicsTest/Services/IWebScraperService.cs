using System;
using System.Collections.Generic;
using forensicsTest.Models;

namespace forensicsTest.Services
{

    public interface IWebScraperService
    {
        public Record ScrapeWebPage(WebsiteModel website);
    }
}
