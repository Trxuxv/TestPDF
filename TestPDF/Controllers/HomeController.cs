using System;
using System.IO;
using System.Web.Mvc;

namespace TestPDF.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public class Html
        {
            public string data { get; set; }
        }

        public ActionResult Convert(string html)
        {
            if(html == null)
            {
                throw new Exception("Html is empty.");
            }
            SautinSoft.PdfMetamorphosis p = new SautinSoft.PdfMetamorphosis();

            string headerInHtml = "";
            p.PageSettings.Size.Auto();
            p.PageSettings.Header.FromString(headerInHtml, SautinSoft.PdfMetamorphosis.HeadersFooters.InputFormat.Html);
            string footerInHtml = "";
            p.PageSettings.Footer.FromString(footerInHtml, SautinSoft.PdfMetamorphosis.HeadersFooters.InputFormat.Html);
            p.PageSettings.Numbering.Text = "";

            var result = p.HtmlToPdfConvertStringToByte(html);

            MemoryStream ms = new MemoryStream(result);

            return new FileStreamResult(ms, "application/pdf");
        }
    }
}