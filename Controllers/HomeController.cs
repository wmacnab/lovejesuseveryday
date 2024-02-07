
using lovejesuseveryday.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace lovejesuseveryday.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }





        /************************************************************************************/
        /* Function to implement daily content */
        public IActionResult Index()
        {
            // Get the current date
            DateTime currentDate = DateTime.Now;
            // Determine the day of the year
            int dayOfYear = currentDate.DayOfYear;
            // Format the date as "Month Day"
            string formattedDate = currentDate.ToString("MMMM d") + GetDaySuffix(currentDate.Day);

            // Create the ViewModel
            var viewModel = new DateContentViewModel();
            viewModel.FormattedDate = formattedDate;
            viewModel.TitleContents = new List<string>();
            viewModel.ChapterContents = new List<string>();



            // Find chapters
            int g = Helper.GlobalChapterStartNum(dayOfYear);
            LoadChapter(g, viewModel.TitleContents, viewModel.ChapterContents);
            LoadChapter(g + 1, viewModel.TitleContents, viewModel.ChapterContents);
            LoadChapter(g + 2, viewModel.TitleContents, viewModel.ChapterContents);
            if (dayOfYear % 4 == 0) LoadChapter(g + 3, viewModel.TitleContents, viewModel.ChapterContents);
            if (dayOfYear == 365) LoadChapter(g + 4, viewModel.TitleContents, viewModel.ChapterContents);



            // Find a chapter
            /*
            string chapterData = Helper.FindChapter(Helper.GlobalChapterStartNum(dayOfYear));
            string[] pair = chapterData.Split(' ');
            pair[0] = Helper.FullBookName(pair[0]);
            string bookId = pair[0];
            string chapterId = pair[1];

            // Get chapter content
            string chapterContent = "";

            // Load XML file
            string translation = "kjv";
            string fileName = $"C:\\Users\\willi\\source\\repos\\lovejesuseveryday\\wwwroot\\eng-{translation}_xml.xml";
            using (XmlReader reader = XmlReader.Create(fileName))
            {
                reader.ReadToFollowing("XMLBIBLE");
                reader.ReadToDescendant("BIBLEBOOK");
                while(bookId != reader.GetAttribute("bname"))
                {

                    reader.ReadToNextSibling("BIBLEBOOK");
                }
                // Now the reader is at the correct chapter
                reader.ReadToDescendant("VERS");
                do {
                    chapterContent += String.Format("{0} {1}<br>", reader.GetAttribute("vnumber").ToString(), reader.ReadInnerXml());
                    reader.ReadToNextSibling("VERS");
                } while (reader.NodeType == XmlNodeType.Element);
            }

            // Add content to the ViewModel
            viewModel.TitleContents.Add($"{bookId} {chapterId}");
            viewModel.ChapterContents.Add(chapterContent);
            */
            /********************************/





            // Pass the ViewModel to the view
            return View(viewModel);
        }

        private void LoadChapter(int globalIndex, List<String> t, List<String> c) // t is title, c is chapter
        {
            string chapterData = Helper.FindChapter(globalIndex);
            string[] pair = chapterData.Split(' ');
            pair[0] = Helper.FullBookName(pair[0]);
            string bookId = pair[0];
            string chapterId = pair[1];

            // Get chapter content
            string chapterContent = "";

            // Load XML file
            string translation = "kjv";
            //string fileName = $"C:\\Users\\willi\\source\\repos\\lovejesuseveryday\\wwwroot\\eng-{translation}_xml.xml";
            string fileName = $"wwwroot/eng-{translation}_xml.xml";
            using (XmlReader reader = XmlReader.Create(fileName))
            {
                reader.ReadToFollowing("XMLBIBLE");
                reader.ReadToDescendant("BIBLEBOOK");
                while(bookId != reader.GetAttribute("bname"))
                {

                    reader.ReadToNextSibling("BIBLEBOOK");
                }
                // Now the reader is at the correct book
                reader.ReadToDescendant("CHAPTER");
                while (chapterId != reader.GetAttribute("cnumber"))
                {
                    reader.ReadToNextSibling("CHAPTER");
                }
                // Now the reader is at the correct chapter
                reader.ReadToDescendant("VERS");
                do {
                    chapterContent += String.Format("{0} {1}<br>", reader.GetAttribute("vnumber").ToString(), reader.ReadInnerXml());
                    reader.ReadToNextSibling("VERS");
                } while (reader.NodeType == XmlNodeType.Element);
            }

            // Add content to the ViewModel
            t.Add($"{bookId} {chapterId}");
            c.Add(chapterContent);
        }

        // Extension method
        private string GetDaySuffix(int day)
        {
            if (day >= 11 && day <= 13)
            {
                return "th";
            }

            switch (day % 10)
            {
                case 1:
                    return "st";
                case 2:
                    return "nd";
                case 3:
                    return "rd";
                default:
                    return "th";
            }
        }


        /************************************************************************************/





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