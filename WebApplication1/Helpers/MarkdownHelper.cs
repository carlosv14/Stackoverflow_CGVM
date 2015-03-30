using System.Web;
using System.Web.Mvc;
using MarkdownSharp;

namespace WebApplication1.Helpers
{
   
    public static partial class MarkdownHelper
    {
      
        public static IHtmlString Markdown(string text)
        {
         
            var markdownTransformer = new Markdown();
            string html = markdownTransformer.Transform(text);

            return new MvcHtmlString(html);
        }

       
        public static IHtmlString Markdown(this HtmlHelper helper, string text)
        {
            return Markdown(text);
        }
    }
}